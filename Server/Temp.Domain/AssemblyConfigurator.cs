using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Temp.Domain.EventMessaging;
using ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;

namespace ModularHouse.Server.Temp.Domain;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();

        return services;
    }
}