using System.Threading.Tasks;

namespace ModularHouse.Mqtt.Broker.Auth;

public interface IAuthenticationService
{
    Task<bool> ValidateCredentialsAsync(string username, string password);
}