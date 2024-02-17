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

public class CreateRouterCommandHandler : ICommandHandler<CreateRouterCommand>
{
    private readonly IRouterDataSource _dataSource;
    private readonly IRouterRepository _repository;
    private readonly IDomainEventBus _domainEventBus;

    public CreateRouterCommandHandler(
        IRouterDataSource dataSource,
        IRouterRepository repository,
        IDomainEventBus domainEventBus)
    {
        _dataSource = dataSource;
        _repository = repository;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(CreateRouterCommand request, CancellationToken cancellationToken)
    {
        var routerExists = await _dataSource.ExistsByIdAsync(request.Input.Id, cancellationToken);
        if (routerExists)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<Router>(),
                ErrorMessages.AlreadyExistDetails((Router r) => r.Id, request.Input.Id));
        }

        var router = request.Input.AsEntity();
        await _repository.CreateAsync(router, cancellationToken);

        var routerCreatedEvent = new RouterCreatedEvent(router.Id, CurrentTransaction.TransactionId);
        await _domainEventBus.PublishAsync(routerCreatedEvent);
    }
}