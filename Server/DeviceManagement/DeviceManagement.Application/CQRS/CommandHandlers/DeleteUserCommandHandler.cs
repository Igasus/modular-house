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

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IMessageBus _messageBus;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventBus _eventBus;

    public DeleteUserCommandHandler(IMessageBus messageBus, IUserRepository userRepository, IDomainEventBus eventBus)
    {
        _messageBus = messageBus;
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var getUserResponse = await _messageBus.Send<GetUserQuery, GetUserQueryResponse>(new GetUserQuery(command.UserId));
        if (getUserResponse is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<User>());
        }
        
        var deletedUser = _userRepository.Delete(getUserResponse.ToDomain());
        await _userRepository.SaveChangesAsync(cancellationToken);

        await _eventBus.PublishAsync(new UserDeletedEvent(deletedUser.ToUserDeletedDto(), CurrentTransaction.TransactionId));
    }
}