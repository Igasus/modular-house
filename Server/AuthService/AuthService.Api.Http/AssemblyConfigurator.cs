using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ModularHouse.Server.Common.Api.Middlewares;

namespace ModularHouse.Server.AuthService.Api.Http;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureHttpWebAppServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth Service API", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddTransactionMiddleware();
        services.AddExceptionHandlerMiddleware();

        return services;
    }

    public static WebApplication ConfigureHttpWebApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.UseTransactionMiddleware();
        app.UseExceptionHandlerMiddleware();

        return app;
    }
}