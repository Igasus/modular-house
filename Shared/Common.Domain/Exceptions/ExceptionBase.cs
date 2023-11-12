using System;
using System.Collections.Generic;
using System.Net;

namespace Common.Domain.Exceptions;

public abstract class ExceptionBase : Exception
{
    public HttpStatusCode ResponseStatusCode { get; }
    public IReadOnlyList<string> ErrorDetails { get; }

    protected ExceptionBase(HttpStatusCode responseStatusCode, string message, params string[] errorDetails)
        : base(message)
    {
        ResponseStatusCode = responseStatusCode;
        ErrorDetails = errorDetails;
    }
}