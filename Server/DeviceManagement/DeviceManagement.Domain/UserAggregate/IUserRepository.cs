using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public interface IUserRepository
{
    public Task CreateAsync(User user, CancellationToken cancellationToken);
    public Task DeleteAsync(User user, CancellationToken cancellationToken);
}