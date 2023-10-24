using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace DeviceManagement.Tests;

public static class TestUtilities
{
    public static DbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<PostgreSqlContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        return new PostgreSqlContext(options);
    }
}