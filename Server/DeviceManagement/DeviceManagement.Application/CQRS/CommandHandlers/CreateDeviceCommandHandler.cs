using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateDeviceCommandHandler(
    IDeviceDataSource deviceDataSource,
    IDeviceRepository deviceRepository,
    IDomainEventBus eventBus)
    : ICommandHandler<CreateDeviceCommand>
{
    public async Task Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var isDeviceExist = await deviceDataSource.ExistByIdAsync(command.DeviceId, cancellationToken);
        if (isDeviceExist)
        {
            throw new BadRequestException(
                ErrorMessages.AlreadyExist<Device>(),
                ErrorMessages.AlreadyExistDetails((Device d) => d.Id, command.DeviceId));
        }

        var device = new Device { Id = command.DeviceId, AdditionDate = DateTime.UtcNow };
        await deviceRepository.CreateAsync(device, cancellationToken);

        await eventBus.PublishAsync(new DeviceCreatedEvent(device.Id, CurrentTransaction.TransactionId));
    }
}