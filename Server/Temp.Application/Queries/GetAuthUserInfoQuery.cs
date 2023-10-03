using ModularHouse.Server.Temp.Application.Abstractions;
using ModularHouse.Server.Temp.Application.Auth.Dto;

namespace ModularHouse.Server.Temp.Application.Queries;

public record GetAuthUserInfoQuery(string UserNameOrEmail, string Password) : IQuery<AuthUserInfoDto>;
