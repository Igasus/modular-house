using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;

public record DeleteModuleByIdCommand(Guid ModuleId) : ICommand;