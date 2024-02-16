using System;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

public class Module
{
    public Guid Id { get; set; }
    public Guid RouterId { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public Guid LastUpdatedByUserId { get; set; }

    public virtual Router Router { get; set; }
    public virtual Device Device { get; set; }
    public virtual User CreatedByUser { get; set; }
    public virtual User LastUpdatedByUser { get; set; }
}