using System.Threading.Tasks;

namespace ModularHouse.Server.Temp.Application.Auth.Contracts;

public interface IAuthService
{
    Task CreateUserAsync(string userName, string email, string password);
    Task<bool> ValidateCredentialsByUserNameAsync(string userName, string password);
    Task<bool> ValidateCredentialsByEmailAsync(string email, string password);
}