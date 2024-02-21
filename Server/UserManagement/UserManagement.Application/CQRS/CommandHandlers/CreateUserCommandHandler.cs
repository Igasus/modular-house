using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;
using ModularHouse.Server.UserManagement.Domain.UserAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class CreateUserCommandHandler(
    IUserDataSource dataSource,
    IUserRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataSource.GetByEmailAsync(command.Input.Email, cancellationToken);
        if (user is not null)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<User>(),
                ErrorMessages.AlreadyExistDetails((User u) => u.Email, command.Input.Email));
        }

        user = new User { Email = command.Input.Email };
        user.SetPassword(command.Input.Password);

        await repository.CreateAsync(user, cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(user.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(userCreatedEvent);
    }
}