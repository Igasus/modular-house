using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Repositories;

public class UserRepository(PostgreSqlContext context) : IUserRepository
{
    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        var userEntry = await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        userEntry.State = EntityState.Detached;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        var userEntry = context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);

        userEntry.State = EntityState.Detached;
    }
}