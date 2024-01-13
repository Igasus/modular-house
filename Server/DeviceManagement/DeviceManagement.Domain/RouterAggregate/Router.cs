using System;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

public class Router
{
    public Guid Id { get; set; }
    public Guid AreaId { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public Guid LastUpdatedByUserId { get; set; }

    public virtual Area Area { get; set; }
    public virtual Device Device { get; set; }
    public virtual User CreatedByUser { get; set; }
    public virtual User LastUpdatedByUser { get; set; }
}