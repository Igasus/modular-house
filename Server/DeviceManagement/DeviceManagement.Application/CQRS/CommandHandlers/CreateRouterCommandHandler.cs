using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateRouterCommandHandler : ICommandHandler<CreateRouterCommand>
{
    private readonly IRouterRepository _routerRepository;
    private readonly IAreaDataSource _areaDataSource;
    private readonly IDeviceDataSource _deviceDataSource;
    private readonly IDomainEventBus _eventBus;

    public CreateRouterCommandHandler(
        IRouterRepository routerRepository,
        IAreaDataSource areaDataSource, 
        IDeviceDataSource deviceDataSource,
        IDomainEventBus eventBus)
    {
        _routerRepository = routerRepository;
        _areaDataSource = areaDataSource;
        _deviceDataSource = deviceDataSource;
        _eventBus = eventBus;
    }

    public async Task Handle(CreateRouterCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfAreaIsNotExistByIdAsync(command.AreaId, cancellationToken);
        await ThrowIfDeviceIsNotExistByIdAsync(command.DeviceId, cancellationToken);
        await ThrowIfDeviceAlreadyLinkedByIdAsync(command.DeviceId, cancellationToken);
        
        //TODO Add Device security check when DMC is ready.
        //TODO CreatedByUserId and LastUpdatedByUserId must be set by current user session
        var router = new Router
        {
            AreaId = command.AreaId,
            DeviceId = command.DeviceId,
            Name = command.Router.Name,
            Description = command.Router.Description,
            CreationDate = DateTime.UtcNow,
            LastUpdatedDate = DateTime.UtcNow
        };

        await _routerRepository.CreateAsync(router, cancellationToken);
        
        await _eventBus.PublishAsync(new RouterCreatedEvent(router.Id, CurrentTransaction.TransactionId));
    }

    private async Task ThrowIfAreaIsNotExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isAreaExist = await _areaDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isAreaExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, id));
        }
    }

    private async Task ThrowIfDeviceIsNotExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeviceExist = await _deviceDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isDeviceExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Device>(),
                ErrorMessages.NotFoundDetails((Device d) => d.Id, id));
        }
    }

    private async Task ThrowIfDeviceAlreadyLinkedByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeviceAlreadyLinked = await _deviceDataSource.IsAlreadyLinkedByIdAsync(id, cancellationToken);
        if (isDeviceAlreadyLinked)
        {
            throw new BadRequestException(
                ErrorMessages.AlreadyLinked<Device>(),
                ErrorMessages.AlreadyLinkedDetails((Device d) => d.Id, id));
        }
    }
}