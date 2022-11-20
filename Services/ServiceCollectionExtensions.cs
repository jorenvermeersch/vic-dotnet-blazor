﻿namespace Services;
using Microsoft.Extensions.DependencyInjection;
using Shared.Account;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, FakeAccountService>();
        // Add more services here...

        return services;
    }
}