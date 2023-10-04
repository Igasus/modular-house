using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace Shared.DI.Extensions;

public static class InfrastructureDependencies
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostgreSqlContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(Constants.POSTGRE_SQL_SERVER)));

        return services;
    }

    public static IServiceProvider InitializeDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var postgreSqlContext = scope.ServiceProvider.GetRequiredService<PostgreSqlContext>();
        postgreSqlContext.Initialize();

        return services;
    }
}