using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class AreaRepository : IAreaRepository
{
    private readonly PostgreSqlContext _context;

    public AreaRepository(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Area area, CancellationToken cancellationToken = default)
    {
        var areaEntry = await _context.AddAsync(area, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        areaEntry.State = EntityState.Detached;
    }

    public async Task UpdateAsync(Area area, CancellationToken cancellationToken = default)
    {
        var areaEntry = _context.Update(area);
        await _context.SaveChangesAsync(cancellationToken);

        areaEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Area area, CancellationToken cancellationToken = default)
    {
        var areaEntry = _context.Remove(area);
        await _context.SaveChangesAsync(cancellationToken);

        areaEntry.State = EntityState.Detached;
    }
}