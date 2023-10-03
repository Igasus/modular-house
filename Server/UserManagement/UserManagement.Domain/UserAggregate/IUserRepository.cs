using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}