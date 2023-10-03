namespace ModularHouse.Server.Domain.UserAggregate;

public interface IUserDataSource
{
    IQueryable<User> Users { get; }
}