using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularHouse.Server.Common.Api.Middlewares;

namespace ModularHouse.Server.DeviceManagement.Api.Http;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)));

        services
            .AddTransactionMiddleware()
            .AddExceptionHandlerMiddleware();

        return services;
    }

    public static WebApplication ConfigureWebApi(this WebApplication app)
    {
        app.UseTransactionMiddleware();
        app.UseExceptionHandlerMiddleware();

        app.UseRouting();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        return app;
    }
}