using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class ModuleRepository(PostgreSqlContext context) : IModuleRepository
{
    public async Task CreateAsync(Module module, CancellationToken cancellationToken = default)
    {
        var moduleEntry = await context.AddAsync(module, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        moduleEntry.State = EntityState.Detached;
    }

    public async Task UpdateAsync(Module module, CancellationToken cancellationToken = default)
    {
        var moduleEntry = context.Update(module);
        await context.SaveChangesAsync(cancellationToken);

        moduleEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Module module, CancellationToken cancellationToken = default)
    {
        var moduleEntry = context.Remove(module);
        await context.SaveChangesAsync(cancellationToken);

        moduleEntry.State = EntityState.Detached;
    }
}