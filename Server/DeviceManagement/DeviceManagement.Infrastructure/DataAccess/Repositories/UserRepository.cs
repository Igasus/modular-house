using System.Threading;
using System.Threading.Tasks;
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

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}