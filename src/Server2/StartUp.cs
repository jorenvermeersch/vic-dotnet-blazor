using Microsoft.OpenApi.Models;
using Persistence;
using Shared.Account;

namespace BlazorApp1.Server
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }

        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SportStore"));
            //services.AddDbContext<SportStoreDbContext>(options =>
            //    options.UseSqlServer(builder.ConnectionString)
            //        .EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging")));

            //services.AddControllersWithViews().AddFluentValidation(config =>
            //{
            //    config.RegisterValidatorsFromAssemblyContaining<ProductDto.Mutate.Validator>();
            //    config.ImplicitlyValidateChildProperties = true;
            //});
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddRazorPages();
            services.AddScoped<IAccountService, BogusAccountService>();
            services.AddDbContext<VicDBContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VicDBContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sportstore API"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            FakeSeeder dataInitializer = new FakeSeeder(dbContext);
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
}
