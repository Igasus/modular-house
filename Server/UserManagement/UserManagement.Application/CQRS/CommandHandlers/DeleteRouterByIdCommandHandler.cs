using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class DeleteRouterByIdCommandHandler : ICommandHandler<DeleteRouterByIdCommand>
{
    private readonly IRouterDataSource _dataSource;
    private readonly IRouterRepository _repository;
    private readonly IDomainEventBus _domainEventBus;

    public DeleteRouterByIdCommandHandler(
        IRouterDataSource dataSource,
        IRouterRepository repository,
        IDomainEventBus domainEventBus)
    {
        _dataSource = dataSource;
        _repository = repository;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(DeleteRouterByIdCommand command, CancellationToken cancellationToken)
    {
        var router = await _dataSource.GetByIdAsync(command.RouterId, cancellationToken);
        if (router is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router a) => a.Id, command.RouterId));
        }

        await _repository.DeleteAsync(router, cancellationToken);

        var routerDeletedEvent = new RouterDeletedEvent(router.Id, CurrentTransaction.TransactionId);
        await _domainEventBus.PublishAsync(routerDeletedEvent);
    }
}