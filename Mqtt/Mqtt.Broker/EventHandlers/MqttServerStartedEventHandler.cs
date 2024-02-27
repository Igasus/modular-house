using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttServerStartedEventHandler(ILogger<MqttServerStartedEventHandler> logger)
    : IMqttServerStartedEventHandler
{
    public Task HandleAsync(EventArgs args)
    {
        logger.LogInformation("MQTT Server started.");
        
        return Task.CompletedTask;
    }
}