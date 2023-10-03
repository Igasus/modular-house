using ModularHouse.Mqtt.Broker.Auth;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttValidatingConnectionEventHandler : IMqttValidatingConnectionEventHandler
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<MqttValidatingConnectionEventHandler> _logger;

    public MqttValidatingConnectionEventHandler(
        IAuthenticationService authenticationService,
        ILogger<MqttValidatingConnectionEventHandler> logger)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    public async Task HandleAsync(ValidatingConnectionEventArgs args)
    {
        var authenticationResult = await _authenticationService.ValidateCredentialsAsync(args.UserName, args.Password);
        
        if (!authenticationResult)
        {
            args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
            _logger.LogWarning($"Authentication for Client {args.ClientId} failed: Incorrect UserName of Password.");
            
            return;
        }

        args.ReasonCode = MqttConnectReasonCode.Success;
    }
}