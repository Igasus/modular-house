using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ModularHouse.Server.Temp.Application.Auth.Contracts;
using ModularHouse.Server.Temp.Domain.Exceptions;
using ModularHouse.Server.Temp.Domain.UserAggregate;

namespace ModularHouse.Server.Temp.Application.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task CreateUserAsync(string userName, string email, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is not null)
        {
            throw new BadRequestException("User with specified UserName already exists.");
        }

        user = await _userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            throw new BadRequestException("User with specified Email already exists.");
        }

        user = new User
        {
            UserName = userName,
            Email = email
        };
        
        var creationResult = await _userManager.CreateAsync(user, password);
        if (!creationResult.Succeeded)
        {
            var errorMessage = string.Join("; ", creationResult.Errors);

            throw new InternalServerErrorException(errorMessage);
        }
    }

    public async Task<bool> ValidateCredentialsByUserNameAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }

        return ValidatePasswordAsync(user, password);
    }

    public async Task<bool> ValidateCredentialsByEmailAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return false;
        }

        return ValidatePasswordAsync(user, password);
    }

    private bool ValidatePasswordAsync(User user, string password)
    {
        var validationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        
        return validationResult is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}