using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;

public record DeleteRouterByIdCommand(Guid RouterId) : ICommand;