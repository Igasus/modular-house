using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.AreaAggregate;

public interface IAreaRepository
{
    Task CreateAsync(Area area, CancellationToken cancellationToken = default);
    Task DeleteAsync(Area area, CancellationToken cancellationToken = default);
}