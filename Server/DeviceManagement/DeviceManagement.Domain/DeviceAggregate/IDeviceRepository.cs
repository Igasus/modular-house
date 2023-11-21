﻿using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

public interface IDeviceRepository
{
    Task CreateAsync(Device device, CancellationToken cancellationToken);
    Task DeleteAsync(Device device, CancellationToken cancellationToken);
}