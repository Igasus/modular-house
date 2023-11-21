using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.UserAggregate;

public interface IUserDataSource
{
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}