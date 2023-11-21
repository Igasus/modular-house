using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly PostgreSqlContext _context;

    public DeviceRepository(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Device device, CancellationToken cancellationToken)
    {
        var deviceEntry = await _context.Devices.AddAsync(device, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        deviceEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Device device, CancellationToken cancellationToken)
    {
        var deviceEntry = _context.Devices.Remove(device);
        await _context.SaveChangesAsync(cancellationToken);

        deviceEntry.State = EntityState.Detached;
    }
}