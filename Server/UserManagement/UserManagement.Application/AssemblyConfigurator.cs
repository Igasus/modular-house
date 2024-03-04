using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Libraries.InternalMessaging.CQRS;

namespace ModularHouse.Server.UserManagement.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(AssemblyConfigurator).Assembly));
        services.AddCQRS();

        return services;
    }
}