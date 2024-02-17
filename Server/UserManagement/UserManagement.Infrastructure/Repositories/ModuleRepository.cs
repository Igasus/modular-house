using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;
using ModularHouse.Server.UserManagement.Infrastructure.Neo4j;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly IDriver _driver;

    public ModuleRepository(IDriver driver)
    {
        _driver = driver;
    }
    
    public async Task CreateAsync(Module module, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(_driver);

        var query =
            $"CREATE (module:{nameof(Module)} {{ " +
            $"    {nameof(Module.Id)}: $Id, " +
            $"    {nameof(Module.AdditionDate)}: ${nameof(Module.AdditionDate)} }})";

        if (module.AdditionDate == default)
            module.AdditionDate = DateTime.UtcNow;
        
        var parameters = new
        {
            Id = module.Id.ToString(),
            module.AdditionDate
        };

        await connection.Session.RunAsync(query, parameters);
    }

    public async Task DeleteAsync(Module module, CancellationToken cancellationToken = default)
    {
        await using var connection = ConnectionContainer.FromDriver(_driver);

        var query =
            $"MATCH (module:{nameof(Module)} {{ {nameof(Module.Id)}: $Id }}) " +
            "DETACH DELETE module ";
        
        var parameters = new
        {
            Id = module.Id.ToString()
        };

        await connection.Session.RunAsync(query, parameters);
    }
}