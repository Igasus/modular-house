using FluentValidation;
using Microsoft.Extensions.Options;
using ModularHouse.Server.Auth.Application.Dto;
using ModularHouse.Server.Auth.Application.Validation.Options;

namespace ModularHouse.Server.Auth.Application.Validation.Validators;

public class UserCredentialsDtoValidator : AbstractValidator<UserCredentialsDto>
{
    public UserCredentialsDtoValidator(IOptions<ValidationOptions> validationOptions)
    {
        RuleFor(credentials => credentials.Email).EmailAddress();
        RuleFor(credentials => credentials.Password).Password(validationOptions.Value.UserCredentials.Password);
    }
}