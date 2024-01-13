using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class DeleteRouterByIdCommandHandler : ICommandHandler<DeleteRouterByIdCommand>
{
    private readonly IRouterRepository _routerRepository;
    private readonly IRouterDataSource _routerDataSource;
    private readonly IDomainEventBus _eventBus;

    public DeleteRouterByIdCommandHandler(
        IRouterRepository routerRepository,
        IRouterDataSource routerDataSource,
        IDomainEventBus eventBus)
    {
        _routerRepository = routerRepository;
        _routerDataSource = routerDataSource;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteRouterByIdCommand command, CancellationToken cancellationToken)
    {
        var router = await _routerDataSource.GetByIdAsync(command.RouterId, cancellationToken);
        if (router is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router r) => r.Id, command.RouterId));
        }

        await _routerRepository.DeleteAsync(router, cancellationToken);

        await _eventBus.PublishAsync(new RouterDeletedEvent(router.Id, CurrentTransaction.TransactionId));
    }
}