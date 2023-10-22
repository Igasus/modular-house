using System;
using Shared.InternalMessaging.CQRS.Abstractions;

namespace ModularHouse.Server.Temp.Application.Commands;

public record AuthSignUpCommand(Guid TransactionId, string UserName, string Email, string Password) : ICommand;