using System;

namespace ModularHouse.Server.Auth.Infrastructure.AccessToken;

public class AccessTokenOptions
{
    public const string Section = "AccessToken";
    
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public TimeSpan ExpirationTime { get; set; }
}