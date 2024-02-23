using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class ModuleDataSource(PostgreSqlContext context) : IModuleDataSource
{
    public async Task<IReadOnlyList<Module>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Modules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Module> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Modules
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}