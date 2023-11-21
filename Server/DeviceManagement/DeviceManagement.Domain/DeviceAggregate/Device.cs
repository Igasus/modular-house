using System;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

public class Device
{
    public Guid Id { get; set; }
    public DateTime AdditionDate { get; set; }
}