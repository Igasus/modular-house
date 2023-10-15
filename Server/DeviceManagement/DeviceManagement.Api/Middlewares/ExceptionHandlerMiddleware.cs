using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ModularHouse.Server.DeviceManagement.Api.Dto;
using ModularHouse.Server.DeviceManagement.Domain.Exceptions;

namespace ModularHouse.Server.DeviceManagement.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private const string INTERNAL_SERVER_ERROR_MESSAGE = "Internal server error.";
    private static readonly JsonSerializerOptions JSON_WEB_SERIALIZER_OPTIONS = new(JsonSerializerDefaults.Web);

    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
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

        await response.WriteAsJsonAsync(errorDto, JSON_WEB_SERIALIZER_OPTIONS);
    }
}