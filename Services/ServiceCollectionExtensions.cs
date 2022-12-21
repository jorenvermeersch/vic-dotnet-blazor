namespace Services;

using Microsoft.Extensions.DependencyInjection;
using Service.VirtualMachines;
using Services.Accounts;
using Services.Customers;
using Services.FakeInitializer;
using Services.Hosts;
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
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IHostService, HostService>();

        services.AddScoped<IVirtualMachineService, FakeVirtualMachineService>();

        // services.AddScoped<IPortService, FakePortService>();
        services.AddScoped<IPortService, PortService>();
        // services.AddScoped<IProcessorService, FakeProcessorService>();
        services.AddScoped<IProcessorService, ProcessorService>();

        services.AddScoped<IFakeInitializerService, FakeInitializerService>();

        return services;
    }
}