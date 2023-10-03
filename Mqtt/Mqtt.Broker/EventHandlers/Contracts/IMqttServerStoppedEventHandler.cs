using System;

namespace ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

public interface IMqttServerStoppedEventHandler : IMqttServerEventHandler<EventArgs>
{
}