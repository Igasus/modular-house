using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

public interface IMqttClientUnsubscribedTopicEventHandler : IMqttServerEventHandler<ClientUnsubscribedTopicEventArgs>
{
}