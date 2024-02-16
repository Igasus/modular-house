using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

public interface IRouterDataSource
{
    Task<IReadOnlyList<Router>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Router> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken = default);
}