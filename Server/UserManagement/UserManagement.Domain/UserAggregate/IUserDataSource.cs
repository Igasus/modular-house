namespace ModularHouse.Server.UserManagement.Domain.UserAggregate;

public interface IUserDataSource
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync();
}