using Microsoft.Extensions.DependencyInjection;
using Shared.InternalMessaging.DomainEvents;

namespace ModularHouse.Server.Temp.Domain;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
    {
        services.AddDomainEventBus();

        return services;
    }
}