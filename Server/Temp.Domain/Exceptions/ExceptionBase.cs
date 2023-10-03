using System.Net;

namespace ModularHouse.Server.Domain.Exceptions;

public abstract class ExceptionBase : Exception
{
    public HttpStatusCode HttpStatusCode { get; }

    protected ExceptionBase(string message, HttpStatusCode httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}