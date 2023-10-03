using Microsoft.EntityFrameworkCore;

namespace ModularHouse.Server.Temp.Domain.UserAggregate;

public interface IUserRepository
{
    DbSet<User> Users { get; }
    DbContext DbContext { get; }
}