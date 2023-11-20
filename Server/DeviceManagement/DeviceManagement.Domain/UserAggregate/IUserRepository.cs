using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public interface IUserRepository
{
    public Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    public void Delete(User user);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}