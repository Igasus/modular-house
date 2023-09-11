using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.Domain.UserAggregate;

namespace ModularHouse.Server.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IList<User>> GetAllAsync()
    {
        var users = await _userRepository.Users.ToListAsync();

        return users;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
            throw new Exception("User does not exist.");

        return user;
    }

    public async Task<Guid> CreateAsync(User input)
    {
        var userEntry = await _userRepository.Users.AddAsync(input);
        await _userRepository.DbContext.SaveChangesAsync();

        userEntry.State = EntityState.Detached;

        return userEntry.Entity.Id;
    }
}