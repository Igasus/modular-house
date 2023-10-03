using ModularHouse.Server.Application.Abstractions;

namespace ModularHouse.Server.Application.Commands;

public record AuthSignUpCommand(string UserName, string Email, string Password) : ICommand;