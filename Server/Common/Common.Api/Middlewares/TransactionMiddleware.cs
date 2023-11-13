using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Server.Common.Domain;

namespace ModularHouse.Server.Common.Api.Middlewares;

public class TransactionMiddleware : IMiddleware
{
    private const string TransactionIdHeader = "TransactionId";
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var transactionIdAsString = context.Request.Headers[TransactionIdHeader].ToString();
        if (!Guid.TryParse(transactionIdAsString, out var transactionId))
        {
            transactionId = Guid.NewGuid();
            context.Request.Headers[TransactionIdHeader] = transactionId.ToString();
        }

        using (CurrentTransaction.SetTransactionId(transactionId))
        {
            await next(context);
        }
    }
}

public static class TransactionMiddlewareMiddlewareExtensions
{
    public static IServiceCollection AddTransactionMiddleware(this IServiceCollection services)
    {
        services.AddScoped<TransactionMiddleware>();
        return services;
    }
    
    public static IApplicationBuilder UseTransactionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<TransactionMiddleware>();
        return app;
    }
}