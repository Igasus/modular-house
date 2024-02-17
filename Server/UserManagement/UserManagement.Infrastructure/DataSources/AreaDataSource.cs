using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.Neo4j;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.DataSources;

public class AreaDataSource : IAreaDataSource
{
    private readonly IDriver _driver;

    public AreaDataSource(IDriver driver)
    {
        _driver = driver;
    }

    public async Task<Area> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(_driver);

        var query =
            $"MATCH (area:{nameof(Area)} {{{nameof(Area.Id)}: $Id}}) " +
            $"RETURN area.{nameof(Area.Id)} AS {nameof(Area.Id)}, " +
            $"    area.{nameof(Area.AdditionDate)} AS {nameof(Area.AdditionDate)} ";
        var parameters = new { Id = id.ToString() };

        var queryResult = await connection.Session.RunAsync(query, parameters);
        var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
        var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
        if (queryResultAsSingleRecord is null) return null;

        var areaAsJson = JsonSerializer.Serialize(queryResultAsSingleRecord.Values);
        var area = JsonSerializer.Deserialize<Area>(areaAsJson);

        return area;
    }
}