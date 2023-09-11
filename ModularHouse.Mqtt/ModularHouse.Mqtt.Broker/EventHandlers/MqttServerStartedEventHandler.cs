using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttServerStartedEventHandler : IMqttServerStartedEventHandler
{
    private readonly ILogger<MqttServerStartedEventHandler> _logger;

    public MqttServerStartedEventHandler(ILogger<MqttServerStartedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(EventArgs args)
    {
        _logger.LogInformation("MQTT Server started.");
        
        return Task.CompletedTask;
    }
}