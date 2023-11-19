using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.UserManagement.Application.Dto;

namespace ModularHouse.Server.UserManagement.Application.QueryResponses;

public record GetUserByIdQueryResponse(UserDto User) : IQueryResponse;