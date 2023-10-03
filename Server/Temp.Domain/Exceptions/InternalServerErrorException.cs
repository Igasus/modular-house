using System.Net;

namespace ModularHouse.Server.Temp.Domain.Exceptions;

public class InternalServerErrorException : ExceptionBase
{
    public InternalServerErrorException(string message) : base(message, HttpStatusCode.InternalServerError)
    {
    }
}