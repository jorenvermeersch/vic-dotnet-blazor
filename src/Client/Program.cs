using Client.Accounts;
using Client.Customers;
using Client.Hosts;
using Client.Authentication;
using Client.VirtualMachines;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;
using MudBlazor.Services;

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

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IHostService, HostService>();
            builder.Services.AddScoped<IVirtualMachineService, VirtualMachineService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IProcessorService, ProcessorService>();
            builder.Services.AddScoped<IPortService, VirtualMachines.PortService>();

            builder.Services.AddMudServices();
            builder.Services.AddLocalization();

            await builder.Build().RunAsync();
        }
    }
}


