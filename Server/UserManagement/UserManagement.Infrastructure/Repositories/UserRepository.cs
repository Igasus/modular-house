using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDriver _driver;

    public UserRepository(IDriver driver)
    {
        _driver = driver;
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await using var session = _driver.AsyncSession();

        var query =
            $"CREATE (user:{nameof(User)} {{ " +
            $"    {nameof(User.Id)}: $Id, " +
            $"    {nameof(User.Email)}: ${nameof(User.Email)}, " +
            $"    {nameof(User.PasswordHash)}: ${nameof(User.PasswordHash)} }})";

        var userId = user.Id == default
            ? Guid.NewGuid()
            : user.Id;
        
        var parameters = new
        {
            Id = userId.ToString(),
            user.Email,
            user.PasswordHash
        };

        try
        {
            await session.RunAsync(query, parameters);
            user.Id = userId;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        await using var session = _driver.AsyncSession();

        var query =
            $"MATCH (user:{nameof(User)} {{ {nameof(User.Id)}: $Id }}) " +
            $"SET user.{nameof(User.Email)} = ${nameof(User.Email)}," +
            $"    user.{nameof(User.PasswordHash)} = ${nameof(User.PasswordHash)}";
        
        var parameters = new
        {
            Id = user.Id.ToString(),
            user.Email,
            user.PasswordHash
        };

        try
        {
            await session.RunAsync(query, parameters);
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        await using var session = _driver.AsyncSession();

        var query =
            $"MATCH (user:{nameof(User)} {{ {nameof(User.Id)}: $Id }}) " +
            "DETACH DELETE user";
        
        var parameters = new
        {
            Id = user.Id.ToString()
        };

        try
        {
            await session.RunAsync(query, parameters);
        }
        finally
        {
            await session.CloseAsync();
        }
    }
}