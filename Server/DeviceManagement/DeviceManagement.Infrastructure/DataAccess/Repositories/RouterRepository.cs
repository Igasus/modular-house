using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class RouterRepository(PostgreSqlContext context) : IRouterRepository
{
    public async Task CreateAsync(Router router, CancellationToken cancellationToken = default)
    {
        var routerEntry = await context.AddAsync(router, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        routerEntry.State = EntityState.Detached;
    }

    public async Task UpdateAsync(Router router, CancellationToken cancellationToken = default)
    {
        var routerEntry = context.Update(router);
        await context.SaveChangesAsync(cancellationToken);

        routerEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Router router, CancellationToken cancellationToken = default)
    {
        var routerEntry = context.Remove(router);
        await context.SaveChangesAsync(cancellationToken);

        routerEntry.State = EntityState.Detached;
    }
}