using System.Text;
using ModularHouse.Mqtt.Broker.Auth;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventsHandler;

public class MqttServerEventsHandler : IMqttServerEventsHandler
{
    private readonly ILogger<MqttServerEventsHandler> _logger;
    private readonly IAuthenticationService _authenticationService;
    
    public MqttServerEventsHandler(
        ILogger<MqttServerEventsHandler> logger,
        IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    public Task StartedAsync(EventArgs args)
    {
        _logger.LogInformation("MQTT Server started.");
        return Task.CompletedTask;
    }

    public Task StoppedAsync(EventArgs args)
    {
        _logger.LogInformation("MQTT Server stopped.");
        return Task.CompletedTask;
    }

    public Task ClientConnectedAsync(ClientConnectedEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just connected.");
        return Task.CompletedTask;
    }

    public Task ClientDisconnectedAsync(ClientDisconnectedEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just disconnected.");
        return Task.CompletedTask;
    }

    public Task ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just subscribed topic {args.TopicFilter.Topic}.");
        return Task.CompletedTask;
    }

    public Task ClientUnsubscribedTopicAsync(ClientUnsubscribedTopicEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just unsubscribed topic {args.TopicFilter}.");
        return Task.CompletedTask;
    }

    public async Task ValidatingConnectionAsync(ValidatingConnectionEventArgs args)
    {
        var authenticationResult = await _authenticationService.ValidateCredentialsAsync(args.UserName, args.Password);
        
        if (!authenticationResult)
        {
            args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
            return;
        }

        args.ReasonCode = MqttConnectReasonCode.Success;
    }

    public Task InterceptingPublishAsync(InterceptingPublishEventArgs args)
    {
        var payload = args.ApplicationMessage.PayloadSegment.ToArray();
        var payloadAsString = Encoding.UTF8.GetString(payload);
        
        _logger.LogInformation(
            $"Client {args.ClientId} just published \"{payloadAsString}\" on topic {args.ApplicationMessage.Topic}");
        
        return Task.CompletedTask;
    }
}