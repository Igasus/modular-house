namespace ModularHouse.Server.AuthService.Infrastructure.HttpClients;

public record HttpClientsOptions
{
    public const string Section = "HttpClients";
    
    public SystemHttpDetails UMS { get; set; }

    public record SystemHttpDetails
    {
        public string BaseUrl { get; set; }
    }
}