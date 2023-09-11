using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.Domain.UserAggregate;

namespace ModularHouse.Server.Infrastructure;

public class ModularHouseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ModularHouseContext(DbContextOptions<ModularHouseContext> options) : base(options)
    {
    }
}