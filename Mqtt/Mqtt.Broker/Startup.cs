using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularHouse.Mqtt.Broker.Auth;
using ModularHouse.Mqtt.Broker.EventHandlers;
using ModularHouse.Mqtt.Broker.EventHandlers.Contracts;
using MQTTnet.AspNetCore;

namespace ModularHouse.Mqtt.Broker;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedMqttServer(optionsBuilder =>
            optionsBuilder.WithDefaultEndpoint());

        services.AddMqttConnectionHandler();
        services.AddConnections();

        services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.Section));

        services.AddTransient<IMqttServerStartedEventHandler, MqttServerStartedEventHandler>();
        services.AddTransient<IMqttServerStoppedEventHandler, MqttServerStoppedEventHandler>();
        services.AddTransient<IMqttClientConnectedEventHandler, MqttClientConnectedEventHandler>();
        services.AddTransient<IMqttClientDisconnectedEventHandler, MqttClientDisconnectedEventHandler>();
        services.AddTransient<IMqttClientSubscribedTopicEventHandler, MqttClientSubscribedTopicEventHandler>();
        services.AddTransient<IMqttClientUnsubscribedTopicEventHandler, MqttClientUnsubscribedTopicEventHandler>();
        services.AddTransient<IMqttValidatingConnectionEventHandler, MqttValidatingConnectionEventHandler>();
        services.AddTransient<IMqttInterceptingPublishEventHandler, MqttInterceptingPublishEventHandler>();

        services.AddTransient<IAuthenticationService, AuthenticationService>();
    }

    public void Configure(
        IApplicationBuilder app,
        IMqttServerStartedEventHandler mqttServerStartedEventHandler,
        IMqttServerStoppedEventHandler mqttServerStoppedEventHandler,
        IMqttClientConnectedEventHandler mqttClientConnectedEventHandler,
        IMqttClientDisconnectedEventHandler mqttClientDisconnectedEventHandler,
        IMqttClientSubscribedTopicEventHandler mqttClientSubscribedTopicEventHandler,
        MqttClientUnsubscribedTopicEventHandler mqttClientUnsubscribedTopicEventHandler,
        IMqttValidatingConnectionEventHandler mqttValidatingConnectionEventHandler,
        IMqttInterceptingPublishEventHandler mqttInterceptingPublishEventHandler)
    {
        app.UseRouting();

        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapConnectionHandler<MqttConnectionHandler>(
                    "/mqtt",
                    httpConnectionDispatcherOptions => httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector =
                        protocolList => protocolList.FirstOrDefault() ?? string.Empty);
            });

        app.UseMqttServer(
            server =>
            {
                server.StartedAsync += mqttServerStartedEventHandler.HandleAsync;
                server.StoppedAsync += mqttServerStoppedEventHandler.HandleAsync;
                server.ClientConnectedAsync += mqttClientConnectedEventHandler.HandleAsync;
                server.ClientDisconnectedAsync += mqttClientDisconnectedEventHandler.HandleAsync;
                server.ClientSubscribedTopicAsync += mqttClientSubscribedTopicEventHandler.HandleAsync;
                server.ClientUnsubscribedTopicAsync += mqttClientUnsubscribedTopicEventHandler.HandleAsync;
                server.ValidatingConnectionAsync += mqttValidatingConnectionEventHandler.HandleAsync;
                server.InterceptingPublishAsync += mqttInterceptingPublishEventHandler.HandleAsync;
            });
    }
}