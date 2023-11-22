﻿using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;

public record GetAreasQuery : IQuery<GetAreasQueryResponse>;