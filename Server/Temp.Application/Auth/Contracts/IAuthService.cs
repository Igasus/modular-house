using System.Threading.Tasks;
using ModularHouse.Server.Temp.Domain.UserAggregate;

namespace ModularHouse.Server.Temp.Application.Auth.Contracts;

public interface IAuthService
{
    Task<User> CreateUserAsync(string userName, string email, string password);
    Task<bool> ValidateCredentialsByUserNameAsync(string userName, string password);
    Task<bool> ValidateCredentialsByEmailAsync(string email, string password);
}