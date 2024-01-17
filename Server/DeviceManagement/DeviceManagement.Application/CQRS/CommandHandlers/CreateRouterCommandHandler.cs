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
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateRouterCommandHandler : ICommandHandler<CreateRouterCommand>
{
    private readonly IRouterRepository _routerRepository;
    private readonly IUserDataSource _userDataSource;
    private readonly IAreaDataSource _areaDataSource;
    private readonly IDeviceDataSource _deviceDataSource;
    private readonly IDomainEventBus _eventBus;

    public CreateRouterCommandHandler(
        IRouterRepository routerRepository,
        IUserDataSource userDataSource,
        IAreaDataSource areaDataSource, 
        IDeviceDataSource deviceDataSource,
        IDomainEventBus eventBus)
    {
        _routerRepository = routerRepository;
        _userDataSource = userDataSource;
        _areaDataSource = areaDataSource;
        _deviceDataSource = deviceDataSource;
        _eventBus = eventBus;
    }

    public async Task Handle(CreateRouterCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfUserIsNotExistByIdAsync(command.UserId, cancellationToken);
        await ThrowIfAreaIsNotExistByIdAsync(command.AreaId, cancellationToken);
        await ThrowIfDeviceIsNotExistByIdAsync(command.DeviceId, cancellationToken);
        
        var router = new Router
        {
            AreaId = command.AreaId,
            DeviceId = command.DeviceId,
            Name = command.Router.Name,
            Description = command.Router.Description,
            CreationDate = DateTime.UtcNow,
            CreatedByUserId = command.UserId,
            LastUpdatedDate = DateTime.UtcNow,
            LastUpdatedByUserId = command.UserId
        };

        await _routerRepository.CreateAsync(router, cancellationToken);
        
        await _eventBus.PublishAsync(new RouterCreatedEvent(router.Id, CurrentTransaction.TransactionId));
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
}