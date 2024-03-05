using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Auth.Application.HttpClients.UMS;
using ModularHouse.Server.Auth.Infrastructure.HttpClients;

namespace ModularHouse.Server.Auth.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<HttpClientsOptions>(configuration.GetSection(HttpClientsOptions.Section));

        services.AddScoped<IUserHttpClient, UserHttpClient>();

        return services;
    }
}