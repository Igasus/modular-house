using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Shared.Models.Responses;

namespace ModularHouse.Server.Common.Api.Middlewares;

public sealed class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(IHostEnvironment environment, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandlerExceptionAsync(exception, context);
        }
    }

    private async Task HandlerExceptionAsync(Exception exception, HttpContext context)
    {
        var errorMessage = exception.Message;
        var errorDetails = Array.Empty<string>();
        var transactionId = CurrentTransaction.TransactionId;
        
        if (exception is ExceptionBase customException)
        {
            context.Response.StatusCode = (int)customException.ResponseStatusCode;

            if (customException.ResponseStatusCode is not HttpStatusCode.InternalServerError)
                errorDetails = customException.ErrorDetails.ToArray();
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        
        var errorResponse = new ErrorResponse(errorMessage, errorDetails, transactionId);
        if (_environment.IsDevelopment())
            errorResponse = errorResponse.WithErrorStackTrace(exception.StackTrace);

        var errorResponseAsJson = JsonSerializer.Serialize(
            errorResponse,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        _logger.LogError(exception, errorResponseAsJson);
        
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(errorResponseAsJson);
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IServiceCollection AddExceptionHandlerMiddleware(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlerMiddleware>();
        return services;
    }
    
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }
}