﻿using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.Dto;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;

public record GetModuleByIdQueryResponse(ModuleDto Module) : IQueryResponse;