using ModularHouse.Mqtt.Broker.Auth;
using ModularHouse.Mqtt.Broker.EventsHandler;
using MQTTnet.AspNetCore;

namespace ModularHouse.Mqtt.Broker;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedMqttServer(optionsBuilder =>
            optionsBuilder.WithDefaultEndpoint());

        services.AddMqttConnectionHandler();
        services.AddConnections();

        services.Configure<AuthOptions>(_configuration.GetSection(AuthOptions.Section));

        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IMqttServerEventsHandler, MqttServerEventsHandler>();
    }

    public void Configure(IApplicationBuilder app, IMqttServerEventsHandler mqttServerEventsHandler)
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
                server.StartedAsync += mqttServerEventsHandler.StartedAsync;
                server.StoppedAsync += mqttServerEventsHandler.StoppedAsync;
                server.ClientConnectedAsync += mqttServerEventsHandler.ClientConnectedAsync;
                server.ClientDisconnectedAsync += mqttServerEventsHandler.ClientDisconnectedAsync;
                server.ClientSubscribedTopicAsync += mqttServerEventsHandler.ClientSubscribedTopicAsync;
                server.ClientUnsubscribedTopicAsync += mqttServerEventsHandler.ClientUnsubscribedTopicAsync;
                server.ValidatingConnectionAsync += mqttServerEventsHandler.ValidatingConnectionAsync;
                server.InterceptingPublishAsync += mqttServerEventsHandler.InterceptingPublishAsync;
            });
    }
}