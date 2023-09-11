using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Domain.UserAggregate;
using ModularHouse.Server.Infrastructure.Repositories;

namespace ModularHouse.Server.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ModularHouseContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ModularHouseSqlServer")));

        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}