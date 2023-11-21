using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserDataSource _userDataSource;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventBus _eventBus;

    public CreateUserCommandHandler(
        IUserDataSource userDataSource,
        IUserRepository userRepository,
        IDomainEventBus eventBus)
    {
        _userDataSource = userDataSource;
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userDataSource.GetByIdAsync(command.UserId, cancellationToken);
        if (existingUser is not null)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<User>());
        }

        var user = new User { Id = command.UserId, AdditionDate = DateTime.UtcNow };
        await _userRepository.CreateAsync(user, cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(user.Id, CurrentTransaction.TransactionId);
        await _eventBus.PublishAsync(userCreatedEvent);
    }
}