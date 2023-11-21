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

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserDataSource _userDataSource;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventBus _eventBus;

    public DeleteUserCommandHandler(
        IUserDataSource userDataSource,
        IUserRepository userRepository,
        IDomainEventBus eventBus)
    {
        _userDataSource = userDataSource;
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userDataSource.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<User>(),
                ErrorMessages.NotFoundDetails((User u) => u.Id, command.UserId));
        }

        await _userRepository.DeleteAsync(user, cancellationToken);

        await _eventBus.PublishAsync(new UserDeletedEvent(user.Id, CurrentTransaction.TransactionId));
    }
}