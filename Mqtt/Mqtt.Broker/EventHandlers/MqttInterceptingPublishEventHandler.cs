using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttInterceptingPublishEventHandler(ILogger<MqttInterceptingPublishEventHandler> logger)
    : IMqttInterceptingPublishEventHandler
{
    public Task HandleAsync(InterceptingPublishEventArgs args)
    {
        var payload = args.ApplicationMessage.PayloadSegment.ToArray();
        var payloadAsString = Encoding.UTF8.GetString(payload);

        logger.LogInformation(
            $"Client {args.ClientId} just published \"{payloadAsString}\" on topic {args.ApplicationMessage.Topic}");

        return Task.CompletedTask;
    }
}