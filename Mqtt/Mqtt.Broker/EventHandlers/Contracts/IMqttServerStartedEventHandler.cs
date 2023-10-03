using System;

namespace ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

public interface IMqttServerStartedEventHandler : IMqttServerEventHandler<EventArgs>
{
}