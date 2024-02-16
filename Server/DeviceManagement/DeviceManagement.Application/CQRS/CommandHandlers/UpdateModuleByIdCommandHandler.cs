using System;
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

public class UpdateModuleByIdCommandHandler : ICommandHandler<UpdateModuleByIdCommand>
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IModuleDataSource _moduleDataSource;
    private readonly IDomainEventBus _domainEventBus;

    public UpdateModuleByIdCommandHandler(
        IModuleRepository moduleRepository,
        IModuleDataSource moduleDataSource, 
        IDomainEventBus domainEventBus)
    {
        _moduleRepository = moduleRepository;
        _moduleDataSource = moduleDataSource;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(UpdateModuleByIdCommand command, CancellationToken cancellationToken)
    {
        var module = await _moduleDataSource.GetByIdAsync(command.ModuleId, cancellationToken);
        if (module is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Module>(),
                ErrorMessages.NotFoundDetails((Module m) => m.Id, command.ModuleId));
        }

        //TODO LastUpdatedByUserId must be set by CurrentUserSession
        module.Name = command.Module.Name;
        module.Description = command.Module.Description;
        module.LastUpdatedDate = DateTime.UtcNow;

        await _moduleRepository.UpdateAsync(module, cancellationToken);

        await _domainEventBus.PublishAsync(new ModuleUpdatedEvent(module.Id, CurrentTransaction.TransactionId));
    }
}