using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.AreaAggregate;

public interface IAreaDataSource
{
    Task<Area> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
}