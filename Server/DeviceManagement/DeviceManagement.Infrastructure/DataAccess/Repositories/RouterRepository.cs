using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class RouterRepository : IRouterRepository
{
    private readonly PostgreSqlContext _context;

    public RouterRepository(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Router router, CancellationToken cancellationToken = default)
    {
        var routerEntry = await _context.AddAsync(router, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        routerEntry.State = EntityState.Detached;
    }

    public async Task UpdateAsync(Router router, CancellationToken cancellationToken = default)
    {
        var routerEntry = _context.Update(router);
        await _context.SaveChangesAsync(cancellationToken);

        routerEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Router router, CancellationToken cancellationToken = default)
    {
        var routerEntry = _context.Remove(router);
        await _context.SaveChangesAsync(cancellationToken);

        routerEntry.State = EntityState.Detached;
    }
}