using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

public sealed class PostgreSqlContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
    {
    }
}