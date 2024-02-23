using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate.Events;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateModuleCommandHandler(
    IModuleRepository moduleRepository,
    IRouterDataSource routerDataSource,
    IDeviceDataSource deviceDataSource,
    IDomainEventBus domainEventBus)
    : ICommandHandler<CreateModuleCommand>
{
    public async Task Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfRouterIsNotExistByIdAsync(command.RouterId, cancellationToken);
        await ThrowIfDeviceIsNotExistByIdAsync(command.DeviceId, cancellationToken);
        await ThrowIfDeviceAlreadyLinkedByIdAsync(command.DeviceId, cancellationToken);
        
        //TODO CreatedByUserId and LastUpdatedByUserId must be set by CurrentUserSession
        var module = new Module
        {
            RouterId = command.RouterId,
            DeviceId = command.DeviceId,
            Name = command.Module.Name,
            Description = command.Module.Description,
            CreationDate = DateTime.UtcNow,
            LastUpdatedDate = DateTime.UtcNow,
        };

        await moduleRepository.CreateAsync(module, cancellationToken);

        await domainEventBus.PublishAsync(new ModuleCreatedEvent(module.Id, CurrentTransaction.TransactionId));
    }

    private async Task ThrowIfRouterIsNotExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isRouterExist = await routerDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isRouterExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router r) => r.Id, id));
        }
    }

    private async Task ThrowIfDeviceIsNotExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeviceExist = await deviceDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isDeviceExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Device>(),
                ErrorMessages.NotFoundDetails((Device d) => d.Id, id));
        }
    }

    private async Task ThrowIfDeviceAlreadyLinkedByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeviceAlreadyLinked = await deviceDataSource.IsAlreadyLinkedByIdAsync(id, cancellationToken);
        if (isDeviceAlreadyLinked)
        {
            throw new BadRequestException(
                ErrorMessages.AlreadyLinked<Device>(),
                ErrorMessages.AlreadyLinkedDetails((Device d) => d.Id, id));
        }
    }
}