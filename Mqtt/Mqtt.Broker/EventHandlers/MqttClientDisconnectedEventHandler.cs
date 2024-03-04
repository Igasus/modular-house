using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientDisconnectedEventHandler(ILogger<MqttClientDisconnectedEventHandler> logger)
    : IMqttClientDisconnectedEventHandler
{
    public Task HandleAsync(ClientDisconnectedEventArgs args)
    {
        logger.LogInformation($"Client {args.ClientId} just disconnected.");

        return Task.CompletedTask;
    }
}