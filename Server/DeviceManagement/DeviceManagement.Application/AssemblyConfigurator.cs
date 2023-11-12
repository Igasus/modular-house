﻿using Libraries.InternalMessaging.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.DeviceManagement.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddCQRS(typeof(AssemblyConfigurator).Assembly);
        return services;
    }
}