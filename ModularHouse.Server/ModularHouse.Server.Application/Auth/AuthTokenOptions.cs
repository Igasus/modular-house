namespace ModularHouse.Server.Application.Auth;

public class AuthTokenOptions
{
    public const string Section = "AuthToken";
    
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationTimeSeconds { get; set; }
}