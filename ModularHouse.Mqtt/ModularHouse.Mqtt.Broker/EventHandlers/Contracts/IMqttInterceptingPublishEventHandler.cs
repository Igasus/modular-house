using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

public interface IMqttInterceptingPublishEventHandler : IMqttServerEventHandler<InterceptingPublishEventArgs>
{
}