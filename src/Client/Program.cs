using Client.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.Account;
using Shared.Customer;
using Shared.Host;
using Shared.Port;
using Shared.VirtualMachine;

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
            builder.Services.AddScoped<IHostService, BogusHostService>();
            //builder.Services.AddScoped<IVirtualMachineService, BogusVirtualMachineService>();

            builder.Services.AddScoped<IVirtualMachineService, Client.VirtualMachines.VirtualMachineService>();
            builder.Services.AddScoped<IAccountService, Client.Accounts.AccountService>();

            //builder.Services.AddScoped<IAccountService, Services.Accounts.AccountService>();
            builder.Services.AddScoped<IPortService, PortService>();

            builder.Services.AddLocalization();

            await builder.Build().RunAsync();
        }
    }
}


