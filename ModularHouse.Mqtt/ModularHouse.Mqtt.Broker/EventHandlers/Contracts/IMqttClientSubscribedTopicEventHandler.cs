using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

public interface IMqttClientSubscribedTopicEventHandler : IMqttServerEventHandler<ClientSubscribedTopicEventArgs>
{
}