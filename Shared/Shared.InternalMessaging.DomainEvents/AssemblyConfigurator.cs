using Microsoft.Extensions.DependencyInjection;
using Shared.InternalMessaging.DomainEvents.Abstractions;

namespace Shared.InternalMessaging.DomainEvents;

public static class AssemblyConfigurator
{
    public static IServiceCollection AddDomainEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();

        return services;
    }
}