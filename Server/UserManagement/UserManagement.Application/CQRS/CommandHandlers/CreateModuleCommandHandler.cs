using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class CreateModuleCommandHandler(
    IModuleDataSource dataSource,
    IModuleRepository repository,
    IDomainEventBus domainEventBus)
    : ICommandHandler<CreateModuleCommand>
{
    public async Task Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        var moduleExists = await dataSource.ExistsByIdAsync(command.Input.Id, cancellationToken);
        if (moduleExists)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<Module>(),
                ErrorMessages.AlreadyExistDetails((Module m) => m.Id, command.Input.Id));
        }

        var module = command.Input.AsEntity();
        await repository.CreateAsync(module, cancellationToken);

        var moduleCreatedEvent = new ModuleCreatedEvent(module.Id, CurrentTransaction.TransactionId);
        await domainEventBus.PublishAsync(moduleCreatedEvent);
    }
}