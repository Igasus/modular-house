using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientSubscribedTopicEventHandler : IMqttClientSubscribedTopicEventHandler
{
    private readonly ILogger<MqttClientSubscribedTopicEventHandler> _logger;

    public MqttClientSubscribedTopicEventHandler(ILogger<MqttClientSubscribedTopicEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ClientSubscribedTopicEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just subscribed topic {args.TopicFilter.Topic}.");
        
        return Task.CompletedTask;
    }
}