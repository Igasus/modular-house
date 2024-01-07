using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate.Events;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class UpdateAreaByIdCommandHandler : ICommandHandler<UpdateAreaByIdCommand>
{
    private readonly IAreaDataSource _areaDataSource;
    private readonly IAreaRepository _areaRepository;
    private readonly IDomainEventBus _eventBus;

    public UpdateAreaByIdCommandHandler(
        IAreaDataSource areaDataSource,
        IAreaRepository areaRepository,
        IDomainEventBus eventBus)
    {
        _areaDataSource = areaDataSource;
        _areaRepository = areaRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(UpdateAreaByIdCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfAreaIsNotExistAsync(command.AreaId, cancellationToken);

        var area = await _areaDataSource.GetByIdAsync(command.AreaId, cancellationToken);
        area.Name = command.Area.Name;
        area.Description = command.Area.Description;
        area.LastUpdatedDate = DateTime.UtcNow;
        area.LastUpdatedByUserId = command.Area.UserId;

        await _areaRepository.UpdateAsync(area, cancellationToken);

        await _eventBus.PublishAsync(new AreaUpdatedEvent(area.Id, CurrentTransaction.TransactionId));
    }
    
    private async Task ThrowIfAreaIsNotExistAsync(Guid id, CancellationToken cancellationToken)
    {
        var isAreaExist = await _areaDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isAreaExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, id));
        }
    }
}