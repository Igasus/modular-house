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

public class DeleteRouterByIdCommandHandler(
    IRouterDataSource dataSource,
    IRouterRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<DeleteRouterByIdCommand>
{
    public async Task Handle(DeleteRouterByIdCommand command, CancellationToken cancellationToken)
    {
        var router = await dataSource.GetByIdAsync(command.RouterId, cancellationToken);
        if (router is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router a) => a.Id, command.RouterId));
        }

        await repository.DeleteAsync(router, cancellationToken);

        var routerDeletedEvent = new RouterDeletedEvent(router.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(routerDeletedEvent);
    }
}