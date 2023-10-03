using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.Temp.Domain.UserAggregate;
using ModularHouse.Server.Temp.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.Temp.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ModularHouseContext _dbContext;

    public DbSet<User> Users => _dbContext.Users;
    public DbContext DbContext => _dbContext;

    public UserRepository(ModularHouseContext dbContext)
    {
        _dbContext = dbContext;
    }
}