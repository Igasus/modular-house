using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class CreateRouterCommandHandler(
    IRouterDataSource dataSource,
    IRouterRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<CreateRouterCommand>
{
    public async Task Handle(CreateRouterCommand command, CancellationToken cancellationToken)
    {
        var routerExists = await dataSource.ExistsByIdAsync(command.Input.Id, cancellationToken);
        if (routerExists)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<Router>(),
                ErrorMessages.AlreadyExistDetails((Router r) => r.Id, command.Input.Id));
        }

        var router = command.Input.AsEntity();
        await repository.CreateAsync(router, cancellationToken);

        var routerCreatedEvent = new RouterCreatedEvent(router.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(routerCreatedEvent);
    }
}