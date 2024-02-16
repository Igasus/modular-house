using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

public interface IAreaDataSource
{
    Task<IReadOnlyList<Area>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Area> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken = default);
}