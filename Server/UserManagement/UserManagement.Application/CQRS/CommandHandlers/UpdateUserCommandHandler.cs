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

public class UpdateUserCommandHandler(
    IUserDataSource dataSource,
    IUserRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataSource.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<User>(),
                ErrorMessages.NotFoundDetails((User u) => u.Id, command.UserId));
        }
        
        user.Email = command.Input.Email;
        user.SetPassword(command.Input.Password);

        await repository.UpdateAsync(user, cancellationToken);

        var userUpdatedEvent = new UserUpdatedEvent(user.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(userUpdatedEvent);
    }
}