using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

public interface IDeviceRepository
{
    Task CreateAsync(Device device, CancellationToken cancellationToken = default);
    Task DeleteAsync(Device device, CancellationToken cancellationToken = default);
}