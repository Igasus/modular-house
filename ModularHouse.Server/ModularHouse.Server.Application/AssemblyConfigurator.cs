using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Application.Auth;
using ModularHouse.Server.Application.Auth.Contracts;

namespace ModularHouse.Server.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(AssemblyConfigurator).Assembly));

        services.Configure<AuthTokenOptions>(options =>
            configuration.GetSection(AuthTokenOptions.Section).Bind(options));

        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}