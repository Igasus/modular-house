using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventBus _eventBus;

    public CreateUserCommandHandler(IMediator mediator, IUserRepository userRepository, IDomainEventBus eventBus)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _mediator.Send(new GetUserQuery(request.Id), cancellationToken);
        if (existingUser != null)
        {
            throw new BadRequestException($"User already exists with given Id: {request.Id}.");
        }

        var user = CreateUserFromRequest(request);

        await _userRepository.Users.AddAsync(user, cancellationToken);
        await _userRepository.Context.SaveChangesAsync(cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(user, CurrentTransaction.TransactionId);
        await _eventBus.PublishAsync(userCreatedEvent);
    }

    private static User CreateUserFromRequest(CreateUserCommand request)
    {
        return new User { Id = request.Id, AdditionDate = DateTime.UtcNow };
    }
}