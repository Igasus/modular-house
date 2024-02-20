using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class DeleteAreaByIdCommandHandler(
    IAreaDataSource dataSource,
    IAreaRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<DeleteAreaByIdCommand>
{
    public async Task Handle(DeleteAreaByIdCommand command, CancellationToken cancellationToken)
    {
        var area = await dataSource.GetByIdAsync(command.AreaId, cancellationToken);
        if (area is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, command.AreaId));
        }

        await repository.DeleteAsync(area, cancellationToken);

        var areaDeletedEvent = new AreaDeletedEvent(area.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(areaDeletedEvent);
    }
}