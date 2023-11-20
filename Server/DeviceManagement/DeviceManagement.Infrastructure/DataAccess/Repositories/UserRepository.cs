using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostgreSqlContext _context;

    public UserRepository(PostgreSqlContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        var userEntry = _context.Entry(user);

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        userEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        var userEntry = _context.Entry(user);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        userEntry.State = EntityState.Detached;
    }
}