using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientUnsubscribedTopicEventHandler : IMqttClientUnsubscribedTopicEventHandler
{
    private readonly ILogger<MqttClientUnsubscribedTopicEventHandler> _logger;

    public MqttClientUnsubscribedTopicEventHandler(ILogger<MqttClientUnsubscribedTopicEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ClientUnsubscribedTopicEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just unsubscribed topic {args.TopicFilter}.");
        
        return Task.CompletedTask;
    }
}