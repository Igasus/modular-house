using ModularHouse.Server.Temp.Domain.UserAggregate;

namespace ModularHouse.Server.Temp.Application.Auth.Dto;

public record AuthUserInfoDto(AuthTokenDto Token, User User);