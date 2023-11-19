using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.QueryResponses;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;

public record GetUserQuery(Guid UserId) : IQuery<GetUserQueryResponse>;