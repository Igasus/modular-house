namespace ModularHouse.Server.Application.Auth;

public record AuthOptions
{
    public const string Section = "Auth";
    
    public TokenOptions Token { get; set; }
    
    public record TokenOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationTimeSeconds { get; set; }
    }
}