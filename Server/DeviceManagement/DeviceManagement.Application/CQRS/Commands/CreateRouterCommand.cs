using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Server.DeviceManagement.Application.Dto;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;

public record CreateRouterCommand(RouterInputDto Router, Guid AreaId, Guid DeviceId) : ICommand;