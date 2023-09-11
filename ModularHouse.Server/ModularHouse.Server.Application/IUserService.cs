using ModularHouse.Server.Domain.UserAggregate;

namespace ModularHouse.Server.Application;

public interface IUserService
{
    Task<IList<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(User input);
}