using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class AreaRepository(PostgreSqlContext context) : IAreaRepository
{
    public async Task CreateAsync(Area area, CancellationToken cancellationToken = default)
    {
        var areaEntry = await context.AddAsync(area, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        areaEntry.State = EntityState.Detached;
    }

    public async Task UpdateAsync(Area area, CancellationToken cancellationToken = default)
    {
        var areaEntry = context.Update(area);
        await context.SaveChangesAsync(cancellationToken);

        areaEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Area area, CancellationToken cancellationToken = default)
    {
        var areaEntry = context.Remove(area);
        await context.SaveChangesAsync(cancellationToken);

        areaEntry.State = EntityState.Detached;
    }
}