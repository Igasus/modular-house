using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

public interface IModuleDataSource
{
    Task<IReadOnlyList<Module>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Module> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}