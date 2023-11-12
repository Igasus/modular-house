using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Libraries.InternalMessaging.DomainEvents;

namespace ModularHouse.Server.DeviceManagement.Domain;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
    {
        services.AddDomainEventBus();
        return services;
    }
}