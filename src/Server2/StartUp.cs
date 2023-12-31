﻿using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Data;
using Services;
using Services.FakeInitializer;
using Shared.Validation;

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

        services.AddServices();


        services.AddControllersWithViews().AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblyContaining<AccountValidator>();
            config.RegisterValidatorsFromAssemblyContaining<HostValidator>();
            config.RegisterValidatorsFromAssemblyContaining<VirtualMachineValidator>();
            config.RegisterValidatorsFromAssemblyContaining<CustomerValidator>();
            config.ImplicitlyValidateChildProperties = true;
        });

        // Database context. 
        var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SqlServer"));
        services.AddDbContextFactory<VicDbContext>(options =>
        {
            options
            .UseSqlServer(builder.ConnectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure())
            .EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging"));
        });

        // Swagger. 
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
        services.AddScoped<IDatabaseSeeder, DemoSeeder>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VicDbContext dbContext, IDatabaseSeeder dataInitializer, IFakeInitializerService fakeInitializerService)
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
            // TODO: Mogelijks uitzetten om op poort 80 te runnen
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
