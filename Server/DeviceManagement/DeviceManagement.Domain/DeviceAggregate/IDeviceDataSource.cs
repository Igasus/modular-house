using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

public interface IDeviceDataSource
{
    Task<IReadOnlyList<Device>> GetAllAsync(CancellationToken cancellationToken);
    Task<Device> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}