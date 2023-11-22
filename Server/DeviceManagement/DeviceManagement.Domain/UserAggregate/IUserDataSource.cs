using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public interface IUserDataSource
{
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Guid>> GetAreaIdsAsync(Guid id, CancellationToken cancellationToken = default);
}