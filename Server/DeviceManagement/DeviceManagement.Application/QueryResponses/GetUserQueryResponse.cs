using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.Dto;

namespace ModularHouse.Server.DeviceManagement.Application.QueryResponses;

public record GetUserQueryResponse(GetUserDto User) : IQueryResponse;