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

public class CreateDeviceCommandHandler : ICommandHandler<CreateDeviceCommand>
{
    private readonly IDeviceDataSource _deviceDataSource;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IDomainEventBus _eventBus;

    public CreateDeviceCommandHandler(
        IDeviceDataSource deviceDataSource,
        IDeviceRepository deviceRepository,
        IDomainEventBus eventBus)
    {
        _deviceDataSource = deviceDataSource;
        _deviceRepository = deviceRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var isDeviceExist = await _deviceDataSource.ExistByIdAsync(command.DeviceId, cancellationToken);
        if (isDeviceExist)
        {
            throw new BadRequestException(
                ErrorMessages.AlreadyExist<Device>(),
                ErrorMessages.AlreadyExistDetails((Device d) => d.Id, command.DeviceId));
        }

        var device = new Device { Id = command.DeviceId, AdditionDate = DateTime.UtcNow };
        await _deviceRepository.CreateAsync(device, cancellationToken);

        await _eventBus.PublishAsync(new DeviceCreatedEvent(device.Id, CurrentTransaction.TransactionId));
    }
}