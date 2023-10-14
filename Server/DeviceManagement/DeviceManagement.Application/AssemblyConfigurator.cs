using Microsoft.Extensions.DependencyInjection;

namespace ModularHouse.Server.DeviceManagement.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        //TODO call this method from DeviceManagement.Startup
        
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(AssemblyConfigurator).Assembly));
    
        return services;
    }
}