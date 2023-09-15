using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModularHouse.Server.Application.Auth;
using ModularHouse.Server.Application.Auth.Contracts;
using ModularHouse.Server.Application.Auth.Dto;
using ModularHouse.Server.Domain.UserAggregate;

namespace ModularHouse.Server.Infrastructure.Auth;

public class AuthJwtManager : IAuthTokenManager
{
    private readonly AuthTokenOptions _authTokenOptions;

    public AuthJwtManager(IOptions<AuthTokenOptions> authTokenOptions)
    {
        _authTokenOptions = authTokenOptions.Value;
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