using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.DataSources;

public class UserDataSource : IUserDataSource
{
    private readonly IDriver _driver;

    public UserDataSource(IDriver driver)
    {
        _driver = driver;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByIdAsync()
    {
        throw new NotImplementedException();
    }
}