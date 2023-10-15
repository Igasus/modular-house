using Microsoft.EntityFrameworkCore;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

public sealed class PostgreSqlContext : DbContext
{
    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options) { }
}