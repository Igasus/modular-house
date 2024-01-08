using System;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

public class Area
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public Guid LastUpdatedByUserId { get; set; }

    public virtual User CreatedByUser { get; set; }
    public virtual User LastUpdatedByUser { get; set; }
}