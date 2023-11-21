﻿using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
}