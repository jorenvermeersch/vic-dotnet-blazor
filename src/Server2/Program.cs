//using Microsoft.AspNetCore.ResponseCompression;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

//var app = builder.Build();




//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseWebAssemblyDebugging();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


//app.UseHttpsRedirection();

//app.UseBlazorFrameworkFiles();
//app.UseStaticFiles();

//app.UseRouting();


//app.MapRazorPages();
//app.MapControllers();
//app.MapFallbackToFile("index.html");

//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<StartUp>();
            });
    }
}