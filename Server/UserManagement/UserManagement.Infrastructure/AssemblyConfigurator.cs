using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.ConfigurationOptions;
using ModularHouse.Server.UserManagement.Infrastructure.DataSources;
using ModularHouse.Server.UserManagement.Infrastructure.Repositories;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<Neo4jOptions>(configuration.GetSection(Neo4jOptions.Section));

        services.AddScoped(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<Neo4jOptions>>().Value;
            var authToken = AuthTokens.Basic(options.Auth.UserName, options.Auth.Password);
            return GraphDatabase.Driver(options.Uri, authToken);
        });
        
        services.AddTransient<IUserDataSource, UserDataSource>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}