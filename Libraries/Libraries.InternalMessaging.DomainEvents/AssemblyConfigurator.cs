using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Libraries.InternalMessaging.DomainEvents;

public static class AssemblyConfigurator
{
    /// <summary>
    /// Configure Services for DomainEvents. <br/>
    /// Will not work if MediatR Services are not configured.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDomainEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();

        return services;
    }
}