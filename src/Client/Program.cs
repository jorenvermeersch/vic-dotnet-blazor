using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Shared;
using Shared.customer;
using Shared.VirtualMachine;
using Shared.Customer;
using Domain.Core;
using Shared.Host;
using Shared.Account;
using Shared.Port;
using Services.Accounts;


namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");


            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
                //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

           

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<FakeAuthenticationProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());

            builder.Services.AddScoped<ICustomerService, BogusCustomerService>();
            builder.Services.AddScoped<IVirtualMachineService, BogusVirtualMachineService>();
            builder.Services.AddScoped<IHostService, BogusHostService>();
            builder.Services.AddScoped<IAccountService, Client.Pages.Account.AccountService>();
            builder.Services.AddScoped<IPortService, PortService>();

            builder.Services.AddLocalization();

            await builder.Build().RunAsync();
        }
    }
}


