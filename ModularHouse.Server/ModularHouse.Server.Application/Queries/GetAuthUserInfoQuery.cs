using ModularHouse.Server.Application.Abstractions;
using ModularHouse.Server.Application.Auth.Dto;

namespace ModularHouse.Server.Application.Queries;

public record GetAuthUserInfoQuery(string UserNameOrEmail, string Password) : IQuery<AuthUserInfoDto>;
