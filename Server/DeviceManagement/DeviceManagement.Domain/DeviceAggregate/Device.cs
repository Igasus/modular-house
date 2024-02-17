using System;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

public class Device
{
    public Guid Id { get; set; }
    public DateTime AdditionDate { get; set; }

    public virtual Router Router { get; set; }
    public virtual Module Module { get; set; }
}