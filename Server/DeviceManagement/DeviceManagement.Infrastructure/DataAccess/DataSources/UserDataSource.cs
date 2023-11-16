using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.DataSources;

public class UserDataSource : IUserDataSource
{
    private readonly PostgreSqlContext _context;

    public UserDataSource(PostgreSqlContext context)
    {
        _context = context;
    }

    public IQueryable<User> Users => _context.Users.AsNoTracking();
}