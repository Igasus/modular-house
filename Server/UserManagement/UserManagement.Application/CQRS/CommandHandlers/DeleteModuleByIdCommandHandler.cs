using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class DeleteModuleByIdCommandHandler(
    IModuleDataSource dataSource,
    IModuleRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<DeleteModuleByIdCommand>
{
    public async Task Handle(DeleteModuleByIdCommand command, CancellationToken cancellationToken)
    {
        var module = await dataSource.GetByIdAsync(command.ModuleId, cancellationToken);
        if (module is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Module>(),
                ErrorMessages.NotFoundDetails((Module a) => a.Id, command.ModuleId));
        }

        await repository.DeleteAsync(module, cancellationToken);

        var moduleDeletedEvent = new ModuleDeletedEvent(module.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(moduleDeletedEvent);
    }
}