namespace ModularHouse.Server.UserManagement.Infrastructure.Neo4j;

public record Neo4jOptions
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