using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;

public record CreateUserCommand(Guid Id) : ICommand;