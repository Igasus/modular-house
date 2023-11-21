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

public class DeleteDeviceCommandHandler : ICommandHandler<DeleteDeviceCommand>
{
    private readonly IDeviceDataSource _deviceDataSource;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IDomainEventBus _eventBus;

    public DeleteDeviceCommandHandler(
        IDeviceDataSource deviceDataSource,
        IDeviceRepository deviceRepository,
        IDomainEventBus eventBus)
    {
        _deviceDataSource = deviceDataSource;
        _deviceRepository = deviceRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var existingDevice = await _deviceDataSource.GetByIdAsync(command.DeviceId, cancellationToken);
        if (existingDevice is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Device>());
        }

        await _deviceRepository.DeleteAsync(existingDevice, cancellationToken);

        await _eventBus.PublishAsync(new DeviceDeletedEvent(existingDevice.Id, CurrentTransaction.TransactionId));
    }
}