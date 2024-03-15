using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Libraries.InternalMessaging.CQRS;
using ModularHouse.Server.Auth.Application.Validation.Options;

namespace ModularHouse.Server.Auth.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ValidationOptions>(configuration.GetSection(ValidationOptions.Section));
        
        services.AddMediatR(mediatorConfiguration =>
            mediatorConfiguration.RegisterServicesFromAssembly(typeof(AssemblyConfigurator).Assembly));
        services.AddCQRS();
        services.AddValidatorsFromAssembly(typeof(AssemblyConfigurator).Assembly);

        return services;
    }
}