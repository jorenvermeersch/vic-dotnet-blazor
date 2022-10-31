using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Domain.Domain;
using Client.Shared;
using Shared.customer;
using Shared.VirtualMachine;
using Shared.Customer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<FakeAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());

builder.Services.AddScoped<ICustomerService, BogusCustomerService>();
builder.Services.AddScoped<IVirtualMachineService, BogusVirtualMachineService>();

await builder.Build().RunAsync();
