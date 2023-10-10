using System;
using ModularHouse.Server.Temp.Application.Abstractions;

namespace ModularHouse.Server.Temp.Application.Commands;

public record AuthSignUpCommand(Guid TransactionId, string UserName, string Email, string Password) : ICommand;