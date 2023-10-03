using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.DataSources;
using ModularHouse.Server.UserManagement.Infrastructure.Repositories;

namespace ModularHouse.Server.UserManagement.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IUserDataSource, UserDataSource>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}