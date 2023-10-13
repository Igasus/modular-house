using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure;

public static class AssemblyConfigurator
{
    private const string POSTGRE_SQL_SERVER = "ModularHousePostgreSqlServer";
    
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostgreSqlContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(POSTGRE_SQL_SERVER)));

        return services;
    }

    public static IServiceProvider InitializeDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var postgreSqlContext = scope.ServiceProvider.GetRequiredService<PostgreSqlContext>();
        postgreSqlContext.Database.Migrate();;

        return services;
    }
}