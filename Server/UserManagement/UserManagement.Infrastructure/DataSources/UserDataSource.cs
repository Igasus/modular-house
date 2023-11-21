using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
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

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
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
            var queryResult = await session.RunAsync(query);
            var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
            foreach (var queryResultItem in queryResultAsList)
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

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var session = _driver.AsyncSession();

        var query = $"MATCH (user:{nameof(User)} {{{nameof(User.Id)}: $Id}}) " +
                    "RETURN " +
                    $"    user.{nameof(User.Id)} AS {nameof(User.Id)}, " +
                    $"    user.{nameof(User.Email)} AS {nameof(User.Email)}, " +
                    $"    user.{nameof(User.PasswordHash)} AS {nameof(User.PasswordHash)} ";
        var parameters = new { Id = id.ToString() };
        
        User user;

        try
        {
            var queryResult = await session.RunAsync(query, parameters);
            var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
            var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
            if (queryResultAsSingleRecord is null) return null;
                    
            var userAsJson = JsonSerializer.Serialize(queryResultAsSingleRecord.Values);
            user = JsonSerializer.Deserialize<User>(userAsJson);
        }
        finally
        {
            await session.CloseAsync();
        }

        return user;
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        await using var session = _driver.AsyncSession();

        var query = $"MATCH (user:{nameof(User)} {{{nameof(User.Email)}: $Email}}) " +
                    "RETURN " +
                    $"    user.{nameof(User.Id)} AS {nameof(User.Id)}, " +
                    $"    user.{nameof(User.Email)} AS {nameof(User.Email)}, " +
                    $"    user.{nameof(User.PasswordHash)} AS {nameof(User.PasswordHash)} ";
        var parameters = new { Email = email };
        
        User user;

        try
        {
            var queryResult = await session.RunAsync(query, parameters);
            var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
            var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
            if (queryResultAsSingleRecord is null) return null;
                    
            var userAsJson = JsonSerializer.Serialize(queryResultAsSingleRecord.Values);
            user = JsonSerializer.Deserialize<User>(userAsJson);
        }
        finally
        {
            await session.CloseAsync();
        }

        return user;
    }
}