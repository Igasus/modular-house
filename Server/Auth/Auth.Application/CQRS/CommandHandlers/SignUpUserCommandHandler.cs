using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Auth.Application.CQRS.Commands;
using ModularHouse.Server.Auth.Application.Dto;
using ModularHouse.Server.Auth.Application.Validation;
using ModularHouse.Server.Auth.Domain.UserAggregate;
using ModularHouse.Server.Auth.Domain.UserAggregate.Events;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;

namespace ModularHouse.Server.Auth.Application.CQRS.CommandHandlers;

public class SignUpUserCommandHandler(
    IUserDataSource dataSource,
    IUserRepository repository,
    IDomainEventBus domainEventBus,
    IValidator<UserCredentialsDto> credentialsValidator) : ICommandHandler<SignUpUserCommand>
{
    public async Task Handle(SignUpUserCommand command, CancellationToken cancellationToken)
    {
        var isEmailTaken = await dataSource.IsExistWithEmail(command.CredentialsDto.Email, cancellationToken);
        if (isEmailTaken)
        {
            throw new BadRequestException(
                ErrorMessages.AlreadyExist<User>(),
                ErrorMessages.AlreadyExistDetails((User u) => u.Email, command.CredentialsDto.Email));
        }

        var validationResult = await credentialsValidator.ValidateAsync(command.CredentialsDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(
                ErrorMessages.ValidationFailed,
                validationResult.Errors.AsErrorMessageDetails<UserCredentialsDto>());
        }

        var user = new User { Email = command.CredentialsDto.Email };
        user.SetPassword(command.CredentialsDto.Password);

        await repository.CreateAsync(user, cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(user.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(userCreatedEvent);
    }
}