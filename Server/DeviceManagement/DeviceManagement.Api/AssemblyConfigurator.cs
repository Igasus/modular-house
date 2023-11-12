using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ModularHouse.Server.DeviceManagement.Api;

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
        
        services.AddTransient<TransactionMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        return services;
    }

    public static WebApplication UseWebApi(this WebApplication app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseTransactionMiddleware();
        app.UseExceptionHandlerMiddleware();
        
        app.MapControllers();

        return app;
    }
}