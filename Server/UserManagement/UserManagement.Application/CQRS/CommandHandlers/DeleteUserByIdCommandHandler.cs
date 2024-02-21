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

public class DeleteUserByIdCommandHandler(
    IUserDataSource dataSource,
    IUserRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<DeleteUserByIdCommand>
{
    public async Task Handle(DeleteUserByIdCommand command, CancellationToken cancellationToken)
    {
        var user = await dataSource.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<User>(),
                ErrorMessages.NotFoundDetails((User u) => u.Id, command.UserId));
        }

        await repository.DeleteAsync(user, cancellationToken);

        var userDeletedEvent = new UserDeletedEvent(user.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(userDeletedEvent);
    }
}