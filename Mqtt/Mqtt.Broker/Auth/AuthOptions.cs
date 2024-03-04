namespace ModularHouse.Mqtt.Broker.Auth;

public record AuthOptions
{
    public const string Section = "Auth";

    public string UserName { get; set; }
    public string Password { get; set; }
}