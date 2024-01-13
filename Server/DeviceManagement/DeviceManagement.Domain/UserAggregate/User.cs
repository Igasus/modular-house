using System;
using System.Collections.Generic;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public class User
{
    public Guid Id { get; set; }
    public DateTime AdditionDate { get; set; }

    public virtual ICollection<Area> CreatedAreas { get; set; }
    public virtual ICollection<Area> LastUpdatedAreas { get; set; }

    public virtual ICollection<Router> CreatedRouters { get; set; }
    public virtual ICollection<Router> LastUpdatedRouters { get; set; }
}