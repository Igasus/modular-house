using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class AreaDataSource(PostgreSqlContext context) : IAreaDataSource
{
    public async Task<IReadOnlyList<Area>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Areas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Area> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Areas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Areas.AnyAsync(x => x.Id == id, cancellationToken);
    }
}