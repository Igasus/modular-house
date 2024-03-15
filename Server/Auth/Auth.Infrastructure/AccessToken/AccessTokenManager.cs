using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ModularHouse.Server.Auth.Application.AccessToken;

namespace ModularHouse.Server.Auth.Infrastructure.AccessToken;

public class AccessTokenManager(IOptions<AccessTokenOptions> options) : IAccessTokenManager
{
    public string CreateJsonWebToken(Guid userId)
    {
        const string securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Secret));
        
        var claims = new Dictionary<string, object>
        {
            ["UserId"] = userId.ToString()
        };
        
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience,
            Claims = claims,
            Expires = DateTime.UtcNow.Add(options.Value.ExpirationTime),
            SigningCredentials = new SigningCredentials(securityKey, securityAlgorithm)
        };

        var handler = new JsonWebTokenHandler();
        var tokenString = handler.CreateToken(descriptor);

        return tokenString;
    }
}