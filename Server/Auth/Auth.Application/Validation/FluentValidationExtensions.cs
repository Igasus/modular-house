using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using ModularHouse.Server.Auth.Application.Validation.Options;

namespace ModularHouse.Server.Auth.Application.Validation;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> Password<T>(
        this IRuleBuilder<T, string> rule,
        PasswordValidationOptions passwordOptions)
    { 
        var options = rule
            .NotEmpty()
            .MinimumLength(passwordOptions.MinimumLength)
            .MaximumLength(passwordOptions.MaximumLength);
        
        if (passwordOptions.MustContainUppercaseEnglishLetter)
        {
            options = options
                .ContainsUppercaseEnglishLetter()
                .WithMessage("'Password' must contain at least one uppercase letter");
        }
        
        if (passwordOptions.MustContainLowercaseEnglishLetter)
        {
            options = options
                .ContainsLowercaseEnglishLetter()
                .WithMessage("'Password' must contain at least one lowercase letter");
        }
        
        if (passwordOptions.MustContainDigit)
        {
            options = options
                .ContainsDigit()
                .WithMessage("'Password' must contain at least one digit");
        }

        if (passwordOptions.MustContainSpecialCharacter)
        {
            options = options
                .ContainsAnyCharacterOf(passwordOptions.AllowedSpecialCharacters)
                .WithMessage("'Password' must contain at least one special character: " +
                             $"{passwordOptions.AllowedSpecialCharacters}");
        }

        return options;
    }

    public static IRuleBuilderOptions<T, string> ContainsUppercaseEnglishLetter<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Must(password => password.Any(char.IsUpper));
    }
    
    public static IRuleBuilderOptions<T, string> ContainsLowercaseEnglishLetter<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Must(password => password.Any(char.IsLower));
    }
    
    public static IRuleBuilderOptions<T, string> ContainsDigit<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.Must(password => password.Any(char.IsDigit));
    }
    
    public static IRuleBuilderOptions<T, string> ContainsAnyCharacterOf<T>(
        this IRuleBuilder<T, string> rule,
        string characters)
    {
        return rule.Must(password => characters.Any(password.Contains));
    }

    public static string AsErrorMessageDetails<TSource>(this List<ValidationFailure> errors)
    {
        var message = new StringBuilder();
        message.Append(typeof(TSource).Name);
        message.AppendLine(" validation failed with errors:");

        foreach (var error in errors)
        {
            message.Append(" - ");
            message.Append(error.PropertyName);
            message.Append(": ");
            message.AppendLine(error.ErrorMessage);
        }

        return message.ToString();
    }
}