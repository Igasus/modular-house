using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}