using Microsoft.EntityFrameworkCore;

namespace ModularHouse.Server.Domain.UserAggregate;

public interface IUserRepository
{
    DbSet<User> Users { get; }
    DbContext DbContext { get; }
}