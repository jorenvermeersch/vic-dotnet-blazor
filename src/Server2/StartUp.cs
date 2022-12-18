
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Data;
using Services;
using Services.FakeInitializer;
using System.Security.Claims;

namespace Server;

public class StartUp
{
    public IConfiguration Configuration { get; }

    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });
        var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("VirtualItCompany"));
        services.AddServices();

        


        services.AddDbContextFactory<VicDBContext>(options =>
            options.UseSqlServer(builder.ConnectionString, opt => opt.EnableRetryOnFailure()).EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging"))
        );

        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.FullName.Replace("+", "."));
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Virtual IT Company API", Version = "v1" });
            c.EnableAnnotations();

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "Using the Authorization header with the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
              { securitySchema, new[] { "Bearer" } }
          });
        });


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = Configuration["Auth0:Authority"];
            options.Audience = Configuration["Auth0:ApiIdentifier"];
        });

        services.AddAuth0AuthenticationClient(config =>
        {
            config.Domain = Configuration["Auth0:Authority"];
            config.ClientId = Configuration["Auth0:ClientId"];
            config.ClientSecret = Configuration["Auth0:ClientSecret"];
        });

        services.AddAuth0ManagementClient().AddManagementAccessToken();


        services.AddRazorPages();
        services.AddScoped<FakeSeeder>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VicDBContext dbContext, FakeSeeder dataInitializer, IFakeInitializerService fakeInitializerService)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Virtual IT Company API");
            });
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        dataInitializer.Seed();

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
