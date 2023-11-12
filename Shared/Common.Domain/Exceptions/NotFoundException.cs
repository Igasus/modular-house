using System.Net;

namespace Common.Domain.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message, params string[] errorDetails)
        : base(HttpStatusCode.NotFound, message, errorDetails)
    {
    }
}