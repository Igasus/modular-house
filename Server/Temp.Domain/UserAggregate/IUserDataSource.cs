namespace ModularHouse.Server.Temp.Domain.UserAggregate;

public interface IUserDataSource
{
    IQueryable<User> Users { get; }
}