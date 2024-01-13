using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

public interface IRouterRepository
{
    Task CreateAsync(Router router, CancellationToken cancellationToken = default);
    Task UpdateAsync(Router router, CancellationToken cancellationToken = default);
    Task DeleteAsync(Router router, CancellationToken cancellationToken = default);
}