using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class DeviceDataSource(PostgreSqlContext context) : IDeviceDataSource
{
    public async Task<IReadOnlyList<Device>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Devices
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Device> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Devices.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsAlreadyLinkedByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Devices.AnyAsync(x => (x.Id == id) 
                                                    && (x.Router != null || x.Module != null), cancellationToken);
    }
}