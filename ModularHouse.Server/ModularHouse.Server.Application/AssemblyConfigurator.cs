using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}