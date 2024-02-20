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

public class DeleteAreaByIdCommandHandler : ICommandHandler<DeleteAreaByIdCommand>
{
    private readonly IAreaDataSource _dataSource;
    private readonly IAreaRepository _repository;
    private readonly IDomainEventBus _domainEventBus;

    public DeleteAreaByIdCommandHandler(
        IAreaDataSource dataSource,
        IAreaRepository repository,
        IDomainEventBus domainEventBus)
    {
        _dataSource = dataSource;
        _repository = repository;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(DeleteAreaByIdCommand command, CancellationToken cancellationToken)
    {
        var area = await _dataSource.GetByIdAsync(command.AreaId, cancellationToken);
        if (area is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, command.AreaId));
        }

        await _repository.DeleteAsync(area, cancellationToken);

        var areaDeletedEvent = new AreaDeletedEvent(area.Id, CurrentTransaction.TransactionId);
        await _domainEventBus.PublishAsync(areaDeletedEvent);
    }
}