using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.RouterAggregate;

public interface IRouterDataSource
{
    public Task<Router> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
}