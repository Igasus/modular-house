using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.Auth.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}