using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularHouse.Server.Common.Api.Middlewares;

namespace ModularHouse.Server.UserManagement.Api.Http;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransactionMiddleware()
            .AddExceptionHandlerMiddleware();

        return services;
    }

    public static IApplicationBuilder ConfigureWebApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseTransactionMiddleware()
            .UseExceptionHandlerMiddleware();

        return app;
    }
}