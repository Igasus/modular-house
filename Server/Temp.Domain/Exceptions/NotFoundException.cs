using System.Net;

namespace ModularHouse.Server.Temp.Domain.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}