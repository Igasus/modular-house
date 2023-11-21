using System;
using System.Collections.Generic;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public class User
{
    public Guid Id { get; set; }
    public DateTime AdditionDate { get; set; }

    public ICollection<Area> CreatedAreas { get; set; }
    public ICollection<Area> LastUpdatedAreas { get; set; }
}