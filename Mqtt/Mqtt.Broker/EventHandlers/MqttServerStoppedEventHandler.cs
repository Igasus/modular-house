using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttServerStoppedEventHandler(ILogger<MqttServerStoppedEventHandler> logger)
    : IMqttServerStoppedEventHandler
{
    public Task HandleAsync(EventArgs args)
    {
        logger.LogInformation("MQTT Server stopped.");
        
        return Task.CompletedTask;
    }
}