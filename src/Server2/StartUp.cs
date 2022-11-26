using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Data;
using Services;

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
        services.AddMvc();
        var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("VirtualItCompany"));
        services.AddServices();


        services.AddDbContextFactory<VicDBContext>(options =>
            options.UseSqlServer(builder.ConnectionString, opt => opt.EnableRetryOnFailure()).EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging"))
        );

        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(x => $"{x.FullName}.{x.Name}");
            //c.CustomSchemaIds(x => $"{x.FullName}.{x.Name}");
            //c.CustomSchemaIds(x => $"VirtualMachineDto.{x.Name}");
            

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Virtual IT Company API", Version = "v1" });
            c.EnableAnnotations();
        });

        services.AddRazorPages();

        services.AddScoped<FakeSeeder>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VicDBContext dbContext, FakeSeeder dataInitializer)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Virtual IT Company API"));
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
