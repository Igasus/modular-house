using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;

namespace ModularHouse.Libraries.InternalMessaging.CQRS;

public static class AssemblyConfigurator
{
    /// <summary>
    /// Configure Services for CQRS. <br/>
    /// Will not work if MediatR Services are not configured.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        services.AddScoped<IMessageBus, MessageBus>();

        return services;
    }
}