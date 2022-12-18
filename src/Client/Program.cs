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
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Client.SharedFiles;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient("AuthenticatedServerAPI", client => client.BaseAddress = new Uri("https://localhost:5001/api"))
                  .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>(); //accesstoken wordt hier toegevoegd
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                   .CreateClient("AuthenticatedServerAPI"));

            //builder.Services.AddScoped(sp => new HttpClient
            //{
            //    BaseAddress = new Uri("https://localhost:5001/api")
            //    //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            //});

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Auth0", options.ProviderOptions);
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
            }).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>(); ;

            //builder.Services.AddAuthorizationCore();
            //builder.Services.AddScoped<FakeAuthenticationProvider>();
            //builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());

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


