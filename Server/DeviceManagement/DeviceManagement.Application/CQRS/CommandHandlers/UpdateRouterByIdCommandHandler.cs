using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class UpdateRouterByIdCommandHandler : ICommandHandler<UpdateRouterByIdCommand>
{
    private readonly IRouterRepository _routerRepository;
    private readonly IRouterDataSource _routerDataSource;
    private readonly IUserDataSource _userDataSource;
    private readonly IDomainEventBus _eventBus;

    public UpdateRouterByIdCommandHandler(
        IRouterRepository routerRepository,
        IRouterDataSource routerDataSource,
        IUserDataSource userDataSource,
        IDomainEventBus eventBus)
    {
        _routerRepository = routerRepository;
        _routerDataSource = routerDataSource;
        _userDataSource = userDataSource;
        _eventBus = eventBus;
    }

    public async Task Handle(UpdateRouterByIdCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfUserIsNotExistByIdAsync(command.UserId, cancellationToken);
        
        var router = await _routerDataSource.GetByIdAsync(command.RouterId, cancellationToken);
        if (router is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router r) => r.Id, command.RouterId));
        }

        router.Name = command.Router.Name;
        router.Description = command.Router.Description;
        router.LastUpdatedDate = DateTime.UtcNow;
        router.LastUpdatedByUserId = command.UserId;

        await _routerRepository.UpdateAsync(router, cancellationToken);

        await _eventBus.PublishAsync(new RouterUpdatedEvent(router.Id, CurrentTransaction.TransactionId));
    }

    private async Task ThrowIfUserIsNotExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isUserExist = await _userDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isUserExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<User>(),
                ErrorMessages.NotFoundDetails((User u) => u.Id, id));
        }
    }
}