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

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserDataSource _dataSource;
    private readonly IUserRepository _repository;
    private readonly IDomainEventBus _domainEventBus;

    public CreateUserCommandHandler(
        IUserDataSource dataSource,
        IUserRepository repository,
        IDomainEventBus domainEventBus)
    {
        _dataSource = dataSource;
        _repository = repository;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _dataSource.GetByEmailAsync(command.Input.Email, cancellationToken);
        if (user is not null)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<User>(),
                ErrorMessages.AlreadyExistDetails((User u) => u.Email, command.Input.Email));
        }

        user = new User { Email = command.Input.Email };
        user.SetPassword(command.Input.Password);

        await _repository.CreateAsync(user, cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(user.Id, CurrentTransaction.TransactionId);
        await _domainEventBus.PublishAsync(userCreatedEvent);
    }
}