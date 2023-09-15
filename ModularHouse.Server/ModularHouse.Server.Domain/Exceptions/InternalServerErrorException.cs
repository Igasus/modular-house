using System.Net;

namespace ModularHouse.Server.Domain.Exceptions;

public class InternalServerErrorException : ExceptionBase
{
    public InternalServerErrorException(string message) : base(message, HttpStatusCode.InternalServerError)
    {
    }
}