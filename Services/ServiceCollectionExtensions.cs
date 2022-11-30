namespace Services;

using Microsoft.Extensions.DependencyInjection;
using Service.Accounts;
using Service.VirtualMachines;
using Services.Customers;
using Services.Hosts;
using Services.Ports;
using Services.Processors;
using Shared.Accounts;
using Shared.Customers;
using Shared.Hosts;
using Shared.Ports;
using Shared.VirtualMachines;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, FakeAccountService>();
        services.AddScoped<ICustomerService, FakeCustomerService>();
        services.AddScoped<IHostService, FakeHostService>();
        services.AddScoped<IVirtualMachineService, FakeVirtualMachineService>();

        services.AddScoped<IPortService, FakePortService>();
        services.AddScoped<IProcessorService, FakeProcessorService>();

        return services;
    }
}