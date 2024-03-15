using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.Auth.Domain.UserAggregate;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
}