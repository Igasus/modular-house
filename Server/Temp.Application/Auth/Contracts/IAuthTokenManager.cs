using ModularHouse.Server.Temp.Application.Auth.Dto;
using ModularHouse.Server.Temp.Domain.UserAggregate;

namespace ModularHouse.Server.Temp.Application.Auth.Contracts;

public interface IAuthTokenManager
{
    AuthTokenDto GenerateToken(User user);
}