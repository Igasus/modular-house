using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.Auth.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        return services;
    }
}