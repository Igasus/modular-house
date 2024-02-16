﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class RouterDataSource : IRouterDataSource
{
    private readonly PostgreSqlContext _context;

    public RouterDataSource(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Router>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Routers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Router> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Routers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Routers.AnyAsync(x => x.Id == id, cancellationToken);
    }
}