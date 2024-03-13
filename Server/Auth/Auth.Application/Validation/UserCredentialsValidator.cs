using FluentValidation;
using ModularHouse.Server.Auth.Application.Dto;

namespace ModularHouse.Server.Auth.Application.Validation;

public class UserCredentialsValidator : AbstractValidator<UserCredentials>
{
    public UserCredentialsValidator()
    {
        RuleFor(credentials => credentials.Email).EmailAddress();
        RuleFor(credentials => credentials.Password).Password();
    }
}