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

public class DeleteDeviceCommandHandler(
    IDeviceDataSource deviceDataSource,
    IDeviceRepository deviceRepository,
    IDomainEventBus eventBus)
    : ICommandHandler<DeleteDeviceCommand>
{
    public async Task Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var existingDevice = await deviceDataSource.GetByIdAsync(command.DeviceId, cancellationToken);
        if (existingDevice is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Device>(),
                ErrorMessages.NotFoundDetails((Device d) => d.Id, command.DeviceId));
        }

        await deviceRepository.DeleteAsync(existingDevice, cancellationToken);

        await eventBus.PublishAsync(new DeviceDeletedEvent(existingDevice.Id, CurrentTransaction.TransactionId));
    }
}