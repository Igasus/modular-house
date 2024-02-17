using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.Neo4j;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.DataSources;

public class ModuleDataSource : IModuleDataSource
{
    private readonly IDriver _driver;

    public ModuleDataSource(IDriver driver)
    {
        _driver = driver;
    }

    public async Task<Module> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(_driver);

        var query =
            $"MATCH (module:{nameof(Module)} {{{nameof(Module.Id)}: $Id}}) " +
            $"RETURN module.{nameof(Module.Id)} AS {nameof(Module.Id)}, " +
            $"    module.{nameof(Module.AdditionDate)} AS {nameof(Module.AdditionDate)} ";
        var parameters = new { Id = id.ToString() };

        var queryResult = await connection.Session.RunAsync(query, parameters);
        var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
        var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
        if (queryResultAsSingleRecord is null) return null;
                    
        var moduleAsJson = JsonSerializer.Serialize(queryResultAsSingleRecord.Values);
        var module = JsonSerializer.Deserialize<Module>(moduleAsJson);

        return module;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(_driver);

        var query =
            $"MATCH (module:{nameof(Module)} {{{nameof(Module.Id)}: $Id}}) " +
            $"RETURN module.{nameof(Module.Id)} AS {nameof(Module.Id)}, " +
            $"    module.{nameof(Module.AdditionDate)} AS {nameof(Module.AdditionDate)} ";
        var parameters = new { Id = id.ToString() };

        var queryResult = await connection.Session.RunAsync(query, parameters);
        var queryResultAsList = await queryResult.ToListAsync(cancellationToken);
        var queryResultAsSingleRecord = queryResultAsList.FirstOrDefault();
        
        return queryResultAsSingleRecord is not null;
    }
}