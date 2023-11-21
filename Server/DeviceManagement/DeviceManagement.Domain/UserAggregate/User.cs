using System;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public class User
{
    public Guid Id { get; set; }
    public DateTime AdditionDate { get; set; }
}