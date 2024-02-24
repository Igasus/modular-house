using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientSubscribedTopicEventHandler(ILogger<MqttClientSubscribedTopicEventHandler> logger)
    : IMqttClientSubscribedTopicEventHandler
{
    public Task HandleAsync(ClientSubscribedTopicEventArgs args)
    {
        logger.LogInformation($"Client {args.ClientId} just subscribed topic {args.TopicFilter.Topic}.");
        
        return Task.CompletedTask;
    }
}