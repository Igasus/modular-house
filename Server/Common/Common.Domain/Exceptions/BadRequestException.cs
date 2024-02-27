using System.Net;

namespace ModularHouse.Server.Common.Domain.Exceptions;

public class BadRequestException(string message, params string[] errorDetails)
    : ExceptionBase(HttpStatusCode.BadRequest, message, errorDetails);