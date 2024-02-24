using System;
using System.Collections.Generic;
using System.Net;

namespace ModularHouse.Server.Common.Domain.Exceptions;

public abstract class ExceptionBase(HttpStatusCode responseStatusCode, string message, params string[] errorDetails)
    : Exception(message)
{
    public HttpStatusCode ResponseStatusCode { get; } = responseStatusCode;
    public IReadOnlyList<string> ErrorDetails { get; } = errorDetails;
}