using System;
using System.Collections.Generic;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

namespace ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

public class Router
{
    public Guid Id { get; set; }
    public Guid AreaId { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime AdditionDate { get; set; }

    public virtual Area Area { get; set; }
    public virtual Device Device { get; set; }

    public virtual ICollection<Module> Modules { get; set; }
}