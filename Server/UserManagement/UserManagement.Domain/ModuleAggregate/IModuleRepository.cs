using System.Threading;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.ModuleAggregate;

public interface IModuleRepository
{
    Task CreateAsync(Module module, CancellationToken cancellationToken = default);
    Task DeleteAsync(Module module, CancellationToken cancellationToken = default);
}