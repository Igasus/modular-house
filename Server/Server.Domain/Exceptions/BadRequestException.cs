using System.Net;

namespace ModularHouse.Server.Domain.Exceptions;

public class BadRequestException : ExceptionBase
{
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}