using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.Neo4j;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.Repositories;

public class RouterRepository(IDriver driver) : IRouterRepository
{
    public async Task CreateAsync(Router router, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(driver);

        var query =
            $"CREATE (router:{nameof(Router)} {{ " +
            $"    {nameof(Router.Id)}: $Id, " +
            $"    {nameof(Router.AdditionDate)}: ${nameof(Router.AdditionDate)} }})";

        if (router.AdditionDate == default)
            router.AdditionDate = DateTime.UtcNow;
        
        var parameters = new
        {
            Id = router.Id.ToString(),
            AdditionDate = router.AdditionDate.ToString("O")
        };

        await connection.Session.RunAsync(query, parameters);
    }

    public async Task DeleteAsync(Router router, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(driver);

        var query =
            $"MATCH (router:{nameof(Router)} {{ {nameof(Router.Id)}: $Id }}) " +
            "DETACH DELETE router ";
        
        var parameters = new
        {
            Id = router.Id.ToString()
        };

        await connection.Session.RunAsync(query, parameters);
    }
}