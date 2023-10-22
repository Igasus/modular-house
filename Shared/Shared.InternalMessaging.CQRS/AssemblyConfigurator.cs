﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.InternalMessaging.CQRS;

public static class AssemblyConfigurator
{
    public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}