using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ModularHouse.Server.Auth.Application.AccessToken;
using ModularHouse.Server.Auth.Domain.UserAggregate;
using ModularHouse.Server.Auth.Infrastructure.AccessToken;
using ModularHouse.Server.Auth.Infrastructure.DataSources;
using ModularHouse.Server.Auth.Infrastructure.Neo4j;
using ModularHouse.Server.Auth.Infrastructure.Repositories;
using Neo4j.Driver;

namespace ModularHouse.Server.Auth.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<Neo4JOptions>(configuration.GetSection(Neo4JOptions.Section));
        services.AddScoped(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<Neo4JOptions>>().Value;
            var authToken = AuthTokens.Basic(options.Auth.UserName, options.Auth.Password);
            return GraphDatabase.Driver(options.Uri, authToken);
        });

        services.Configure<AccessTokenOptions>(configuration.GetSection(AccessTokenOptions.Section));
        services.AddScoped<IAccessTokenManager, AccessTokenManager>();

        services.AddScoped<IUserDataSource, UserDataSource>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}