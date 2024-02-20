using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class CreateAreaCommandHandler(
    IAreaDataSource dataSource,
    IAreaRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<CreateAreaCommand>
{
    public async Task Handle(CreateAreaCommand command, CancellationToken cancellationToken)
    {
        var areaExists = await dataSource.ExistsByIdAsync(command.Input.Id, cancellationToken);
        if (areaExists)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<Area>(),
                ErrorMessages.AlreadyExistDetails((Area a) => a.Id, command.Input.Id));
        }

        var area = command.Input.AsEntity();
        await repository.CreateAsync(area, cancellationToken);

        var areaCreatedEvent = new AreaCreatedEvent(area.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(areaCreatedEvent);
    }
}