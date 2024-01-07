using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.AuthService.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}