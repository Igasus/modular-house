namespace ModularHouse.Server.Auth.Infrastructure.Neo4j;

public record Neo4JOptions
{
    public const string Section = "Neo4j";

    public string Uri { get; set; }
    public AuthOptions Auth { get; set; }

    public record AuthOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}