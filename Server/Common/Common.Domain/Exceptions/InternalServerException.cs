using System.Net;

namespace ModularHouse.Server.Common.Domain.Exceptions;

public class InternalServerException(string message, params string[] errorDetails)
    : ExceptionBase(HttpStatusCode.InternalServerError, message, errorDetails);