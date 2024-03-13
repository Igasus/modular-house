using System;
using System.Collections.Generic;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

public class Area
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime AdditionDate { get; set; }

    public virtual ICollection<Router> Routers { get; set; }
}