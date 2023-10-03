using System;
using System.Threading.Tasks;

namespace ModularHouse.Mqtt.Broker.EventHandlers.Contracts;

public interface IMqttServerEventHandler<TEventArgs>
    where TEventArgs : EventArgs
{
    Task HandleAsync(TEventArgs args);
}