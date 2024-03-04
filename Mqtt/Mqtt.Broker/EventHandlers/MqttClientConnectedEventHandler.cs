using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientConnectedEventHandler(ILogger<MqttClientConnectedEventHandler> logger)
    : IMqttClientConnectedEventHandler
{
    public Task HandleAsync(ClientConnectedEventArgs args)
    {
        logger.LogInformation($"Client {args.ClientId} just connected.");

        return Task.CompletedTask;
    }
}