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

public class CreateUserCommandHandler(
    IUserDataSource userDataSource,
    IUserRepository userRepository,
    IDomainEventBus eventBus)
    : ICommandHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var isUserExist = await userDataSource.ExistByIdAsync(command.UserId, cancellationToken);
        if (isUserExist)
        {
            throw new BadRequestException(
                ErrorMessages.AlreadyExist<User>(),
                ErrorMessages.AlreadyExistDetails((User u) => u.Id, command.UserId));
        }

        var user = new User { Id = command.UserId, AdditionDate = DateTime.UtcNow };
        await userRepository.CreateAsync(user, cancellationToken);

        await eventBus.PublishAsync(new UserCreatedEvent(user.Id, CurrentTransaction.TransactionId));
    }
}