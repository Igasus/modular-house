using ModularHouse.Server.Temp.Application.Auth.Dto;
using ModularHouse.Server.Temp.Domain.UserAggregate;
using ModularHouse.Shared.Models.Responses.Auth;

namespace ModularHouse.Server.Temp.Api.MappingExtensions;

public static class AuthMappingExtensions
{
    public static AuthSignInResponse ToResponse(this AuthUserInfoDto dto)
    {
        return new AuthSignInResponse(dto.Token.ToResponse(), dto.User.ToAuthResponse());
    }

    public static AuthSignInResponseToken ToResponse(this AuthTokenDto dto) =>
        new AuthSignInResponseToken(dto.Value, dto.ExpirationDate);

    public static AuthSignInResponseUser ToAuthResponse(this User entity) =>
        new AuthSignInResponseUser(entity.Id, entity.UserName);
}