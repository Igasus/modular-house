using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class DeviceDataSource : IDeviceDataSource
{
    private readonly PostgreSqlContext _context;

    public DeviceDataSource(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Device>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Devices
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Device> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Devices.AnyAsync(x => x.Id == id, cancellationToken);
    }
}