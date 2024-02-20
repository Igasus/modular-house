using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.Neo4j;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.DataSources;

public class RouterDataSource : IRouterDataSource
{
    private readonly IDriver _driver;

    public RouterDataSource(IDriver driver)
    {
        _driver = driver;
    }

    public async Task<Router> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(_driver);

        var query =
            $"MATCH (router:{nameof(Router)} {{{nameof(Router.Id)}: $Id}}) " +
            $"RETURN router.{nameof(Router.Id)} AS {nameof(Router.Id)}, " +
            $"    router.{nameof(Router.AdditionDate)} AS {nameof(Router.AdditionDate)} ";
        var parameters = new { Id = id.ToString() };

        var queryResult = await connection.Session.RunAsync(query, parameters);
        var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
        var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
        if (queryResultAsSingleRecord is null) return null;
                    
        var routerAsJson = JsonSerializer.Serialize(queryResultAsSingleRecord.Values);
        var router = JsonSerializer.Deserialize<Router>(routerAsJson);

        return router;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(_driver);

        var query =
            $"MATCH (router:{nameof(Router)} {{{nameof(Router.Id)}: $Id}}) " +
            $"RETURN router.{nameof(Router.Id)} AS {nameof(Router.Id)}, " +
            $"    router.{nameof(Router.AdditionDate)} AS {nameof(Router.AdditionDate)} ";
        var parameters = new { Id = id.ToString() };

        var queryResult = await connection.Session.RunAsync(query, parameters);
        var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
        var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
        
        return queryResultAsSingleRecord is not null;
    }
}