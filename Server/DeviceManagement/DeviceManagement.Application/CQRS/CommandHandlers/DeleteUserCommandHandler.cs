using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventBus _eventBus;

    public DeleteUserCommandHandler(IUserRepository userRepository, IDomainEventBus eventBus)
    {
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var deletedUser = await _userRepository.DeleteAsync(command.UserId, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        await _eventBus.PublishAsync(new UserDeletedEvent(deletedUser.ToUserDeletedDto(), CurrentTransaction.TransactionId));
    }
}