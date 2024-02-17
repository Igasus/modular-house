using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.ModuleAggregate;

public interface IModuleDataSource
{
    Task<Module> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}