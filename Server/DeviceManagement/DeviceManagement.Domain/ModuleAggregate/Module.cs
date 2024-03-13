using System;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

public class Module
{
    public Guid Id { get; set; }
    public Guid RouterId { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime AdditionDate { get; set; }

    public virtual Router Router { get; set; }
    public virtual Device Device { get; set; }
}