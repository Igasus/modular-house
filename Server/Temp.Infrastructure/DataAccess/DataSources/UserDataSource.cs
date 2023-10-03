using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.Temp.Domain.UserAggregate;
using ModularHouse.Server.Temp.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.Temp.Infrastructure.DataAccess.DataSources;

public class UserDataSource : IUserDataSource
{
    private readonly ModularHouseContext _dbContext;

    public UserDataSource(ModularHouseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<User> Users => _dbContext.Users.AsNoTracking();
}