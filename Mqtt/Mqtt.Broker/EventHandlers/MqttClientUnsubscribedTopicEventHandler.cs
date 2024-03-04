using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientUnsubscribedTopicEventHandler(ILogger<MqttClientUnsubscribedTopicEventHandler> logger)
    : IMqttClientUnsubscribedTopicEventHandler
{
    public Task HandleAsync(ClientUnsubscribedTopicEventArgs args)
    {
        logger.LogInformation($"Client {args.ClientId} just unsubscribed topic {args.TopicFilter}.");

        return Task.CompletedTask;
    }
}