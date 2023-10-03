using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModularHouse.Server.Temp.Application.Auth;
using ModularHouse.Server.Temp.Application.Auth.Contracts;
using ModularHouse.Server.Temp.Application.Auth.Dto;
using ModularHouse.Server.Temp.Domain.UserAggregate;

namespace ModularHouse.Server.Temp.Infrastructure.Auth;

public class AuthJwtManager : IAuthTokenManager
{
    private readonly AuthOptions.TokenOptions _authTokenOptions;

    public AuthJwtManager(IOptions<AuthOptions> authOptions)
    {
        _authTokenOptions = authOptions.Value.Token;
    }

    public AuthTokenDto GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authTokenOptions.Secret));
        var expirationDate = DateTimeOffset.UtcNow.AddSeconds(_authTokenOptions.ExpirationTimeSeconds);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = expirationDate.UtcDateTime,
            Issuer = _authTokenOptions.Issuer,
            Audience = _authTokenOptions.Audience,
            SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var authTokenValue = tokenHandler.WriteToken(token);

        return new AuthTokenDto(authTokenValue, expirationDate);
    }
}