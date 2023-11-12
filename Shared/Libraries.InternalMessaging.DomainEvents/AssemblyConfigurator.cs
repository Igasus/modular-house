using Libraries.InternalMessaging.DomainEvents.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Libraries.InternalMessaging.DomainEvents;

public static class AssemblyConfigurator
{
    public static IServiceCollection AddDomainEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();

        return services;
    }
}