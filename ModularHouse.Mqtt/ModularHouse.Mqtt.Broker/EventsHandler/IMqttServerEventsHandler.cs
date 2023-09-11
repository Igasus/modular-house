using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventsHandler;

public interface IMqttServerEventsHandler
{
    Task StartedAsync(EventArgs args);
    Task StoppedAsync(EventArgs args);
    Task ClientConnectedAsync(ClientConnectedEventArgs args);
    Task ClientDisconnectedAsync(ClientDisconnectedEventArgs args);
    Task ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs args);
    Task ClientUnsubscribedTopicAsync(ClientUnsubscribedTopicEventArgs args);
    Task ValidatingConnectionAsync(ValidatingConnectionEventArgs args);
    Task InterceptingPublishAsync(InterceptingPublishEventArgs args);
}