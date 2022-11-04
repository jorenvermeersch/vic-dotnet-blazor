using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Shared;
using Shared.customer;
using Shared.VirtualMachine;
using Shared.Customer;
using Domain.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<FakeAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());

builder.Services.AddScoped<IHostService, BogusCustomerService>();
builder.Services.AddScoped<IVirtualMachineService, BogusVirtualMachineService>();

await builder.Build().RunAsync();
