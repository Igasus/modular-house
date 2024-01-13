using System.Collections.Generic;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.Dto;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;

public record GetRoutersQueryResponse(IReadOnlyList<RouterDto> Routers, int TotalRoutersCount) : IQueryResponse;