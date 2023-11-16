using System.Linq;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

public interface IUserDataSource
{
    public IQueryable<User> Users { get; }
}