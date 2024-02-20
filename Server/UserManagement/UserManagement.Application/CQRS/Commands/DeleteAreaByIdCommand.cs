using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;

namespace ModularHouse.Server.UserManagement.Application.CQRS.Commands;

public record DeleteAreaByIdCommand(Guid AreaId) : ICommand;