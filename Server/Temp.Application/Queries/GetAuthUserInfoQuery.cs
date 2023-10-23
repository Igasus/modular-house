using ModularHouse.Server.Temp.Application.Auth.Dto;
using Shared.InternalMessaging.CQRS.Abstractions;

namespace ModularHouse.Server.Temp.Application.Queries;

public record GetAuthUserInfoQuery(string UserNameOrEmail, string Password) : IQuery<AuthUserInfoDto>;
