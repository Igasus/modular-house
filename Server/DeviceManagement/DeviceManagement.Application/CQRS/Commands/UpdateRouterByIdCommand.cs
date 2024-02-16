using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Server.DeviceManagement.Application.Dto;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;

public record UpdateRouterByIdCommand(Guid RouterId, RouterInputDto Router) : ICommand;