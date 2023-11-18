using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;

namespace ModularHouse.Server.DeviceManagement.Application.QueryResponses;

public record GetUserQueryResponse(Guid Id, DateTime AdditionDate) : IQueryResponse;