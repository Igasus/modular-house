using System.Net;

namespace ModularHouse.Server.Temp.Domain.Exceptions;

public class BadRequestException : ExceptionBase
{
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}