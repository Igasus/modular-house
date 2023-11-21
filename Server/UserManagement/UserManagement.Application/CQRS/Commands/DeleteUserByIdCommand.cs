using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;

namespace ModularHouse.Server.UserManagement.Application.CQRS.Commands;

public record DeleteUserByIdCommand(Guid UserId) : ICommand;