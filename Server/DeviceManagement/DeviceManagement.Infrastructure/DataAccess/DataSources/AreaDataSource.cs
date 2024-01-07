using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class AreaDataSource : IAreaDataSource
{
    private readonly PostgreSqlContext _context;

    public AreaDataSource(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Area>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Areas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Area> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Areas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Areas.AnyAsync(x => x.Name == name, cancellationToken);
    }
}