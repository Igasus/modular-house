using Microsoft.EntityFrameworkCore;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public interface IUserRepository
{
    public DbSet<User> Users { get; }
    public DbContext Context { get; }
}