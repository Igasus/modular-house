using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.Auth.Domain.UserAggregate;
using Neo4j.Driver;

namespace ModularHouse.Server.Auth.Infrastructure.Repositories;

public class UserRepository(IDriver driver) : IUserRepository
{
    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await using var session = driver.AsyncSession();

        const string query = """
            CREATE (user:User {
                Id: $Id,
                Email: $Email,
                PasswordHash: $PasswordHash,
                AdditionDate: $AdditionDate });
        """;

        var userId = user.Id == default
            ? Guid.NewGuid()
            : user.Id;
        var additionDate = DateTime.UtcNow;

        var parameters = new
        {
            Id = userId.ToString(),
            user.Email,
            user.PasswordHash,
            AdditionDate = additionDate.ToString("s")
        };

        await session.RunAsync(query, parameters);
        user.Id = userId;
        user.AdditionDate = additionDate;
    }
}