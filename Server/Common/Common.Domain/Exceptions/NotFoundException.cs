using System.Net;

namespace ModularHouse.Server.Common.Domain.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message, params string[] errorDetails)
        : base(HttpStatusCode.NotFound, message, errorDetails)
    {
    }
}