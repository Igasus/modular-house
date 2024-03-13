using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.Auth.Domain.UserAggregate;

public interface IUserDataSource
{
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsExistWithEmail(string email, CancellationToken cancellationToken = default);
}