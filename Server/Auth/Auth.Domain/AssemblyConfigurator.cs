using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Libraries.InternalMessaging.DomainEvents;

namespace ModularHouse.Server.Auth.Domain;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
    {
        services.AddDomainEventBus();

        return services;
    }
}