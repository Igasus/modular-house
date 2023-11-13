using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Libraries.InternalMessaging.DomainEvents;

public static class AssemblyConfigurator
{
    public static IServiceCollection AddDomainEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();

        return services;
    }
}