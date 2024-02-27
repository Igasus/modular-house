using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ModularHouse.Mqtt.Broker.Auth;

public class AuthenticationService(IOptions<AuthOptions> authOptions) : IAuthenticationService
{
    private readonly AuthOptions _authOptions = authOptions.Value;

    public Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        if (username != _authOptions.UserName || password != _authOptions.Password)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}