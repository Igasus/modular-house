using System.Net;

namespace ModularHouse.Server.Common.Domain.Exceptions;

public class BadRequestException : ExceptionBase
{
    public BadRequestException(string message, params string[] errorDetails) 
        : base(HttpStatusCode.BadRequest, message, errorDetails)
    {
    }
}