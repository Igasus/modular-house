using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateAreaCommandHandler(IAreaRepository areaRepository, IDomainEventBus eventBus)
    : ICommandHandler<CreateAreaCommand>
{
    public async Task Handle(CreateAreaCommand command, CancellationToken cancellationToken)
    {
        //TODO CreatedByUserId and LastUpdatedByUserId must be set by CurrentUserSession

        var area = new Area
        {
            Name = command.Area.Name,
            Description = command.Area.Description,
            CreationDate = DateTime.UtcNow,
            LastUpdatedDate = DateTime.UtcNow
        };

        await areaRepository.CreateAsync(area, cancellationToken);

        await eventBus.PublishAsync(new AreaCreatedEvent(area.Id, CurrentTransaction.TransactionId));
    }
}