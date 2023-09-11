using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientDisconnectedEventHandler : IMqttClientDisconnectedEventHandler
{
    private readonly ILogger<MqttClientDisconnectedEventHandler> _logger;

    public MqttClientDisconnectedEventHandler(ILogger<MqttClientDisconnectedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ClientDisconnectedEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just disconnected.");
        
        return Task.CompletedTask;
    }
}