using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class DeviceRepository(PostgreSqlContext context) : IDeviceRepository
{
    public async Task CreateAsync(Device device, CancellationToken cancellationToken = default)
    {
        var deviceEntry = await context.Devices.AddAsync(device, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        deviceEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Device device, CancellationToken cancellationToken = default)
    {
        var deviceEntry = context.Devices.Remove(device);
        await context.SaveChangesAsync(cancellationToken);

        deviceEntry.State = EntityState.Detached;
    }
}