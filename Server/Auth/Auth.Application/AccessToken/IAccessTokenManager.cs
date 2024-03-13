using System;

namespace ModularHouse.Server.Auth.Application.AccessToken;

public interface IAccessTokenManager
{
    string CreateJsonWebToken(Guid userId);
}