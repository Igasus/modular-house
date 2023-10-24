using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using ModularHouse.Server.DeviceManagement.Api.Dto;
using ModularHouse.Server.DeviceManagement.Domain.Exceptions;

namespace ModularHouse.Server.DeviceManagement.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private const string INTERNAL_SERVER_ERROR_MESSAGE = "Internal server error.";
    private static readonly JsonSerializerOptions JSON_WEB_SERIALIZER_OPTIONS = new(JsonSerializerDefaults.Web);

    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // TODO implement logging

        var response = context.Response;
        response.ContentType = MediaTypeNames.Application.Json;

        var errorDto = new ErrorDto();
        if (ex is DeviceManagementException exception)
        {
            response.StatusCode = (int)exception.StatusCode;
            errorDto.ErrorMessage = exception.Message;
        }
        else
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorDto.ErrorMessage = INTERNAL_SERVER_ERROR_MESSAGE;
        }

        if (_environment.IsDevelopment())
        {
            errorDto.StackTrace = ex.StackTrace;
        }

        await response.WriteAsJsonAsync(errorDto, JSON_WEB_SERIALIZER_OPTIONS);
    }
}