using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

public interface IAreaRepository
{
    Task CreateAsync(Area area, CancellationToken cancellationToken = default);
    Task UpdateAsync(Area area, CancellationToken cancellationToken = default);
    Task DeleteAsync(Area area, CancellationToken cancellationToken = default);
}