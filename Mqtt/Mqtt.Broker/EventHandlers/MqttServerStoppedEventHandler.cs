using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttServerStoppedEventHandler : IMqttServerStoppedEventHandler
{
    private readonly ILogger<MqttServerStoppedEventHandler> _logger;

    public MqttServerStoppedEventHandler(ILogger<MqttServerStoppedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(EventArgs args)
    {
        _logger.LogInformation("MQTT Server stopped.");
        
        return Task.CompletedTask;
    }
}