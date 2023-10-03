using ModularHouse.Server.Application.Auth.Dto;
using ModularHouse.Server.Domain.UserAggregate;

namespace ModularHouse.Server.Application.Auth.Contracts;

public interface IAuthTokenManager
{
    AuthTokenDto GenerateToken(User user);
}