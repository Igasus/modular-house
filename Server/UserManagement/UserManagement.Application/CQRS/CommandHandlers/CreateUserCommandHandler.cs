using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
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

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataSource.GetByEmailAsync(request.Input.Email, cancellationToken);
        if (user is not null)
            // TODO throw AlreadyExistException with proper message
            throw new Exception("User already exist");

        user = new User { Email = request.Input.Email };
        user.SetPassword(request.Input.Password);

        await _repository.CreateAsync(user, cancellationToken);

        await _domainEventBus.PublishAsync(new UserCreatedEvent(user.Id, CurrentTransaction.TransactionId));
    }
}