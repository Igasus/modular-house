using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        await using var session = _driver.AsyncSession();

        var query =
            $"MATCH (user:{nameof(User)}) " +
            "RETURN " +
            $"    user.{nameof(User.Id)} AS {nameof(User.Id)}, " +
            $"    user.{nameof(User.Email)} AS {nameof(User.Email)}, " +
            $"    user.{nameof(User.PasswordHash)} AS {nameof(User.PasswordHash)} ";

        var users = new List<User>();
        
        try
        {
            var queryResultList = await (await session.RunAsync(query)).ToListAsync();
            foreach (var queryResultItem in queryResultList)
            {
                var userAsJson = JsonSerializer.Serialize(queryResultItem.Values);
                var user = JsonSerializer.Deserialize<User>(userAsJson);
                users.Add(user);
            }
        }
        finally
        {
            await session.CloseAsync();
        }

        return users;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        await using var session = _driver.AsyncSession();

        var query = $"MATCH (user:{nameof(User)} {{{nameof(User.Id)}: $id}}) " +
                    "RETURN " +
                    $"    user.{nameof(User.Id)} AS {nameof(User.Id)}, " +
                    $"    user.{nameof(User.Email)} AS {nameof(User.Email)}, " +
                    $"    user.{nameof(User.PasswordHash)} AS {nameof(User.PasswordHash)} ";
        var parameters = new { id = id.ToString() };
        
        User user;

        try
        {
            var queryResult = (await (await session.RunAsync(query, parameters)).ToListAsync()).FirstOrDefault();
            if (queryResult is null) return null;
                    
            var userAsJson = JsonSerializer.Serialize(queryResult.Values);
            user = JsonSerializer.Deserialize<User>(userAsJson);
        }
        finally
        {
            await session.CloseAsync();
        }

        return user;
    }
}