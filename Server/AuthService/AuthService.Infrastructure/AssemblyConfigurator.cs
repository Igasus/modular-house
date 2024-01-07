using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.AuthService.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        return services;
    }
}