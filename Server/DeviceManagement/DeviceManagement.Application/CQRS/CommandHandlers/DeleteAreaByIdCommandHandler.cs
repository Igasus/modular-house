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

public class DeleteAreaByIdCommandHandler : ICommandHandler<DeleteAreaByIdCommand>
{
    private readonly IAreaDataSource _areaDataSource;
    private readonly IAreaRepository _areaRepository;
    private readonly IDomainEventBus _eventBus;

    public DeleteAreaByIdCommandHandler(
        IAreaDataSource areaDataSource,
        IAreaRepository areaRepository,
        IDomainEventBus eventBus)
    {
        _areaDataSource = areaDataSource;
        _areaRepository = areaRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteAreaByIdCommand command, CancellationToken cancellationToken)
    {
        var area = await _areaDataSource.GetByIdAsync(command.AreaId, cancellationToken);
        if (area is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, command.AreaId));
        }

        await _areaRepository.DeleteAsync(area, cancellationToken);

        await _eventBus.PublishAsync(new AreaDeletedEvent(area.Id, CurrentTransaction.TransactionId));
    }
}