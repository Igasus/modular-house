using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.Server;

namespace ModularHouse.Mqtt.Broker.EventHandlers;

public class MqttClientConnectedEventHandler : IMqttClientConnectedEventHandler
{
    private readonly ILogger<MqttClientConnectedEventHandler> _logger;

    public MqttClientConnectedEventHandler(ILogger<MqttClientConnectedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ClientConnectedEventArgs args)
    {
        _logger.LogInformation($"Client {args.ClientId} just connected.");
        
        return Task.CompletedTask;
    }
}