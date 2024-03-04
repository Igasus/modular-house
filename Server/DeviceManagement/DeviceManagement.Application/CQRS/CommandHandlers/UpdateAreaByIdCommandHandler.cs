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

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class UpdateAreaByIdCommandHandler(
    IAreaDataSource areaDataSource,
    IAreaRepository areaRepository,
    IDomainEventBus eventBus)
    : ICommandHandler<UpdateAreaByIdCommand>
{
    public async Task Handle(UpdateAreaByIdCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfAreaIsNotExistAsync(command.AreaId, cancellationToken);

        //TODO area.LastUpdatedByUserId must be set by current UserSession
        var area = await areaDataSource.GetByIdAsync(command.AreaId, cancellationToken);
        area.Name = command.Area.Name;
        area.Description = command.Area.Description;
        area.LastUpdatedDate = DateTime.UtcNow;

        await areaRepository.UpdateAsync(area, cancellationToken);

        await eventBus.PublishAsync(new AreaUpdatedEvent(area.Id, CurrentTransaction.TransactionId));
    }

    private async Task ThrowIfAreaIsNotExistAsync(Guid id, CancellationToken cancellationToken)
    {
        var isAreaExist = await areaDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isAreaExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, id));
        }
    }
}