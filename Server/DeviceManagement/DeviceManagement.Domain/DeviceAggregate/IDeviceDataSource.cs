using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

public interface IDeviceDataSource
{
    Task<IReadOnlyList<Device>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Device> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsAlreadyLinkedByIdAsync(Guid id, CancellationToken cancellationToken = default);
}