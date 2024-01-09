using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.AuthService.Application.HttpClients.UMS;
using ModularHouse.Server.AuthService.Infrastructure.HttpClients;

namespace ModularHouse.Server.AuthService.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<HttpClientsOptions>(configuration.GetSection(HttpClientsOptions.Section));
        
        services.AddTransient<IUserHttpClient, UserHttpClient>();
        
        return services;
    }
}