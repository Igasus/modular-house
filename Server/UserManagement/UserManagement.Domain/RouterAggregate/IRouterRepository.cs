using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.RouterAggregate;

public interface IRouterRepository
{
    Task CreateAsync(Router router, CancellationToken cancellationToken = default);
    Task DeleteAsync(Router router, CancellationToken cancellationToken = default);
}