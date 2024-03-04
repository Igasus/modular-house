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

public sealed class ExceptionHandlerMiddleware(IHostEnvironment environment, ILogger<ExceptionHandlerMiddleware> logger)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        string errorMessage;
        string[] errorDetails;

        if (exception is ExceptionBase customException
            && (environment.IsDevelopment()
                || customException.ResponseStatusCode is not HttpStatusCode.InternalServerError))
        {
            errorMessage = customException.Message;
            errorDetails = customException.ErrorDetails.ToArray();
            context.Response.StatusCode = (int)customException.ResponseStatusCode;
        }
        else
        {
            errorMessage = ErrorMessages.InternalServer;
            errorDetails = Array.Empty<string>();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        var errorResponse = new ErrorResponse(errorMessage, errorDetails, CurrentTransaction.TransactionId);
        if (environment.IsDevelopment())
            errorResponse = errorResponse.WithErrorStackTrace(exception.StackTrace);

        var errorResponseAsJson = JsonSerializer.Serialize(
            errorResponse,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        logger.LogError(exception, errorResponseAsJson);

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