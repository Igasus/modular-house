using System.Net;

namespace ModularHouse.Server.Domain.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}