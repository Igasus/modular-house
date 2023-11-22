using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

namespace ModularHouse.Server.DeviceManagement.Infrastructure;

public static class AssemblyConfigurator
{
    private const string POSTGRE_SQL_SERVER = "ModularHousePostgreSqlServer";

    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostgreSqlContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(POSTGRE_SQL_SERVER)));

        services
            .AddRepositories()
            .AddDataSources();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IDeviceRepository, DeviceRepository>();

        return services;
    }

    private static IServiceCollection AddDataSources(this IServiceCollection services)
    {
        services.AddTransient<IUserDataSource, UserDataSource>();
        services.AddTransient<IDeviceDataSource, DeviceDataSource>();
        services.AddTransient<IAreaDataSource, AreaDataSource>();

        return services;
    }

    public static IServiceProvider InitializeDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var postgreSqlContext = scope.ServiceProvider.GetRequiredService<PostgreSqlContext>();
        postgreSqlContext.Database.Migrate();

        return services;
    }
}