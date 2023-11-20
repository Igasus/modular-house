using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public interface IUserDataSource
{
    public Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
    public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}