using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly PostgreSqlContext _context;

    public ModuleRepository(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Module module, CancellationToken cancellationToken = default)
    {
        var moduleEntry = await _context.AddAsync(module, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        moduleEntry.State = EntityState.Detached;
    }

    public async Task UpdateAsync(Module module, CancellationToken cancellationToken = default)
    {
        var moduleEntry = _context.Update(module);
        await _context.SaveChangesAsync(cancellationToken);

        moduleEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(Module module, CancellationToken cancellationToken = default)
    {
        var moduleEntry = _context.Remove(module);
        await _context.SaveChangesAsync(cancellationToken);

        moduleEntry.State = EntityState.Detached;
    }
}