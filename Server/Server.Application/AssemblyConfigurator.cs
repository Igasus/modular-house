using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Application.Auth;
using ModularHouse.Server.Application.Auth.Contracts;
using ModularHouse.Server.Application.Options;

namespace ModularHouse.Server.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(AssemblyConfigurator).Assembly));

        services.Configure<AuthOptions>(options =>
            configuration.GetSection(AuthOptions.Section).Bind(options));

        services.Configure<IdentityOptions>(options =>
            configuration.GetSection(IdentityOptions.Section).Bind(options));

        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}