using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularHouse.Server.UserManagement.Domain.UserAggregate;

public interface IUserDataSource
{
    Task<IReadOnlyList<User>> GetAllAsync();
    Task<User> GetByIdAsync();
}