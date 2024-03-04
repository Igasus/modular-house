using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.Neo4j;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.Repositories;

public class AreaRepository(IDriver driver) : IAreaRepository
{
    public async Task CreateAsync(Area area, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(driver);

        var query =
            $"CREATE (area:{nameof(Area)} {{ " +
            $"    {nameof(Area.Id)}: $Id, " +
            $"    {nameof(Area.AdditionDate)}: ${nameof(Area.AdditionDate)} }})";

        if (area.AdditionDate == default)
            area.AdditionDate = DateTime.UtcNow;

        var parameters = new
        {
            Id = area.Id.ToString(),
            AdditionDate = area.AdditionDate.ToString("O")
        };

        await connection.Session.RunAsync(query, parameters);
    }

    public async Task DeleteAsync(Area area, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(driver);

        var query =
            $"MATCH (area:{nameof(Area)} {{ {nameof(Area.Id)}: $Id }}) " +
            "DETACH DELETE area ";

        var parameters = new
        {
            Id = area.Id.ToString()
        };

        await connection.Session.RunAsync(query, parameters);
    }
}