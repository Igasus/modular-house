using ModularHouse.Server.UserManagement.Domain.UserAggregate;

namespace ModularHouse.Server.UserManagement.Infrastructure.DataSources;

public class UserDataSource : IUserDataSource
{
    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync()
    {
        throw new NotImplementedException();
    }
}