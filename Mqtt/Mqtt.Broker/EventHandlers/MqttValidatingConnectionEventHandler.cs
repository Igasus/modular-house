using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.Auth;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttValidatingConnectionEventHandler(
    IAuthenticationService authenticationService,
    ILogger<MqttValidatingConnectionEventHandler> logger) : IMqttValidatingConnectionEventHandler
{
    public async Task HandleAsync(ValidatingConnectionEventArgs args)
    {
        var authenticationResult = await authenticationService.ValidateCredentialsAsync(args.UserName, args.Password);

        if (!authenticationResult)
        {
            args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
            logger.LogWarning($"Authentication for Client {args.ClientId} failed: Incorrect UserName of Password.");

            return;
        }

        args.ReasonCode = MqttConnectReasonCode.Success;
    }
}