using System.Net;

namespace ModularHouse.Server.Common.Domain.Exceptions;

public class InternalServerException : ExceptionBase
{
    public InternalServerException(string message, params string[] errorDetails)
        : base(HttpStatusCode.InternalServerError, message, errorDetails)
    {
    }
}