using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Application.Auth.Contracts;
using ModularHouse.Server.Application.Options;
using ModularHouse.Server.Domain.UserAggregate;
using ModularHouse.Server.Infrastructure.Auth;
using ModularHouse.Server.Infrastructure.DataAccess.Database;
using ModularHouse.Server.Infrastructure.DataAccess.DataSources;
using ModularHouse.Server.Infrastructure.DataAccess.Repositories;

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
            var identityOptions = configuration.GetSection(IdentityOptions.Section).Get<IdentityOptions>();
            options.User.RequireUniqueEmail = identityOptions.RequireUniqueEmail;
            options.Password.RequireDigit = identityOptions.Password.RequireDigit;
            options.Password.RequiredLength = identityOptions.Password.RequiredLength;
            options.Password.RequireLowercase = identityOptions.Password.RequireLowercase;
            options.Password.RequireNonAlphanumeric = identityOptions.Password.RequireNonAlphanumeric;
            options.Password.RequireUppercase = identityOptions.Password.RequireUppercase;
            options.Password.RequiredUniqueChars = identityOptions.Password.RequiredUniqueChars;
        }).AddEntityFrameworkStores<ModularHouseContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserDataSource, UserDataSource>();

        services.AddTransient<IAuthTokenManager, AuthJwtManager>();

        return services;
    }
}