using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.Auth.Domain.UserAggregate;
using Neo4j.Driver;

namespace ModularHouse.Server.Auth.Infrastructure.DataSources;

public class UserDataSource(IDriver driver) : IUserDataSource
{
    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        await using var session = driver.AsyncSession();

        const string query = """
            MATCH (user:User {Email: $Email})
            RETURN user.Id AS Id,
                user.Email AS Email,
                user.PasswordHash AS PasswordHash,
                user.AdditionDate AS AdditionDate;
        """;

        var parameters = new { Email = email };

        var queryResult = await session.RunAsync(query, parameters);
        var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
        var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
        if (queryResultAsSingleRecord is null)
            return null;

        var userAsJson = JsonSerializer.Serialize(queryResultAsSingleRecord.Values);
        var user = JsonSerializer.Deserialize<User>(userAsJson);

        return user;
    }

    public async Task<bool> IsExistWithEmail(string email, CancellationToken cancellationToken = default)
    {
        await using var session = driver.AsyncSession();

        const string query = """
            MATCH (user:User {Email: $Email})
            RETURN user;
        """;

        var parameters = new { Email = email };

        var queryResult = await session.RunAsync(query, parameters);
        var queryResultAsList = await queryResult.ToListAsync(cancellationToken);

        return queryResultAsList.Any();
    }
}