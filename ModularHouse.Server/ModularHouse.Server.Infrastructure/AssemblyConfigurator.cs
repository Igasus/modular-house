using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Application.Auth.Contracts;
using ModularHouse.Server.Domain.UserAggregate;
using ModularHouse.Server.Infrastructure.Auth;
using ModularHouse.Server.Infrastructure.DataSources;
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

        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            // TODO Set other Identity options.
        }).AddEntityFrameworkStores<ModularHouseContext>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserDataSource, UserDataSource>();

        services.AddTransient<IAuthTokenManager, AuthJwtManager>();

        return services;
    }
}