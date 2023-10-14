using System;
using System.Net;

namespace ModularHouse.Server.DeviceManagement.Domain.Exceptions;

public abstract class DeviceManagementException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    protected DeviceManagementException(string message) : base(message) { }
}