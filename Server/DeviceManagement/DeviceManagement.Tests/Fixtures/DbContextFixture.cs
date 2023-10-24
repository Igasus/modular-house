using System;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace DeviceManagement.Tests.Fixtures;

public class DbContextFixture : IDisposable
{
    public PostgreSqlContext DatabaseContext { get; }

    public DbContextFixture()
    {
        var options = new DbContextOptionsBuilder<PostgreSqlContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        DatabaseContext = new PostgreSqlContext(options);
        DatabaseContext.Database.EnsureCreated();
    }
    
    public void Dispose()
    {
        DatabaseContext.Database.EnsureDeleted();
        DatabaseContext.Dispose();
    }
}