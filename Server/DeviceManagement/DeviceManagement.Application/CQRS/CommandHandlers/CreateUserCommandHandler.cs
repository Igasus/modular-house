using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Application.QueryResponses;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IMessageBus _messageBus;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventBus _eventBus;

    public CreateUserCommandHandler(IMessageBus messageBus, IUserRepository userRepository, IDomainEventBus eventBus)
    {
        _messageBus = messageBus;
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _messageBus.Send<GetUserQuery, GetUserQueryResponse>(new GetUserQuery(command.UserId));
        if (existingUser != null)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<User>());
        }

        var user = new User { Id = command.UserId, AdditionDate = DateTime.UtcNow };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(createdUser.ToUserCreatedDto(), CurrentTransaction.TransactionId);
        await _eventBus.PublishAsync(userCreatedEvent);
    }
}