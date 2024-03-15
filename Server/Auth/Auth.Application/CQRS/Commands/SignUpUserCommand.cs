using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Server.Auth.Application.Dto;

namespace ModularHouse.Server.Auth.Application.CQRS.Commands;

public record SignUpUserCommand(UserCredentialsDto CredentialsDto) : ICommand;