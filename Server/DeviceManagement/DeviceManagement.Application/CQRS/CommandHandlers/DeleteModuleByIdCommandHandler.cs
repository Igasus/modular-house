using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class DeleteModuleByIdCommandHandler(
    IModuleRepository moduleRepository,
    IModuleDataSource moduleDataSource,
    IDomainEventBus domainEventBus)
    : ICommandHandler<DeleteModuleByIdCommand>
{
    public async Task Handle(DeleteModuleByIdCommand command, CancellationToken cancellationToken)
    {
        var module = await moduleDataSource.GetByIdAsync(command.ModuleId, cancellationToken);
        if (module is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Module>(),
                ErrorMessages.NotFoundDetails((Module m) => m.Id, command.ModuleId));
        }

        await moduleRepository.DeleteAsync(module, cancellationToken);

        await domainEventBus.PublishAsync(new ModuleDeletedEvent(module.Id, CurrentTransaction.TransactionId));
    }
}