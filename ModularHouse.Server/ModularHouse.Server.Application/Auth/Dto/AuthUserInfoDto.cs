using ModularHouse.Server.Domain.UserAggregate;

namespace ModularHouse.Server.Application.Auth.Dto;

public record AuthUserInfoDto(AuthTokenDto Token, User User);