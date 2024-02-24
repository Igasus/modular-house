using System.Net;

namespace ModularHouse.Server.Common.Domain.Exceptions;

public class NotFoundException(string message, params string[] errorDetails)
    : ExceptionBase(HttpStatusCode.NotFound, message, errorDetails);