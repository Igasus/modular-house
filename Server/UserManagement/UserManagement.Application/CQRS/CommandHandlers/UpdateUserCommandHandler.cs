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

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserDataSource _dataSource;
    private readonly IUserRepository _repository;
    private readonly IDomainEventBus _domainEventBus;

    public UpdateUserCommandHandler(
        IUserDataSource dataSource,
        IUserRepository repository,
        IDomainEventBus domainEventBus)
    {
        _dataSource = dataSource;
        _repository = repository;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataSource.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<User>(),
                ErrorMessages.NotFoundDetails((User u) => u.Id, request.UserId));
        }
        
        user.Email = request.Input.Email;
        user.SetPassword(request.Input.Password);

        await _repository.UpdateAsync(user, cancellationToken);

        var userUpdatedEvent = new UserUpdatedEvent(user.Id, CurrentTransaction.TransactionId);
        await _domainEventBus.PublishAsync(userUpdatedEvent);
    }
}