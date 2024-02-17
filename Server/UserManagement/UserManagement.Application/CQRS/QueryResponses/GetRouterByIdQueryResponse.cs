﻿using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.UserManagement.Application.Dto;

namespace ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;

public record GetRouterByIdQueryResponse(RouterDto Router) : IQueryResponse;