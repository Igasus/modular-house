using Libraries.InternalMessaging.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.DeviceManagement.Domain;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
    {
        services.AddDomainEventBus();
        return services;
    }
}