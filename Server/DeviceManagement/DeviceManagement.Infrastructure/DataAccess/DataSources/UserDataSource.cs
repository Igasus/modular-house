using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class UserDataSource : IUserDataSource
{
    private readonly PostgreSqlContext _context;

    public UserDataSource(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetAreaIdsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.CreatedAreas)
            .Where(user => user.Id == id)
            .SelectMany(user => user.CreatedAreas)
            .Select(area => area.Id)
            .ToListAsync(cancellationToken);
    }
}