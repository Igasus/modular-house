using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttInterceptingPublishEventHandler : IMqttInterceptingPublishEventHandler
{
    private readonly ILogger<MqttInterceptingPublishEventHandler> _logger;

    public MqttInterceptingPublishEventHandler(ILogger<MqttInterceptingPublishEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(InterceptingPublishEventArgs args)
    {
        var payload = args.ApplicationMessage.PayloadSegment.ToArray();
        var payloadAsString = Encoding.UTF8.GetString(payload);
        
        _logger.LogInformation(
            $"Client {args.ClientId} just published \"{payloadAsString}\" on topic {args.ApplicationMessage.Topic}");
        
        return Task.CompletedTask;
    }
}