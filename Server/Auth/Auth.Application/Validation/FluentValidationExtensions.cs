using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace ModularHouse.Server.Auth.Application.Validation;

public static class FluentValidationExtensions
{
    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(32)
            .ContainsUppercaseEnglishLetter()
            .ContainsLowercaseEnglishLetter()
            .ContainsDigit()
            .ContainsSpecialCharacter();

        return options;
    }

    private static IRuleBuilder<T, string> ContainsUppercaseEnglishLetter<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .Must(password => password.Any(char.IsUpper))
            .WithMessage("'Password' must contain at least one uppercase letter");

        return options;
    }
    
    private static IRuleBuilder<T, string> ContainsLowercaseEnglishLetter<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .Must(password => password.Any(char.IsLower))
            .WithMessage("'Password' must contain at least one lowercase letter");

        return options;
    }
    
    private static IRuleBuilder<T, string> ContainsDigit<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .Must(password => password.Any(char.IsDigit))
            .WithMessage("'Password' must contain at least one digit");
        
        return options;
    }
    
    private static IRuleBuilder<T, string> ContainsSpecialCharacter<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        const string allowedSpecialCharacters = "!@#$%^&*()-_=+,./\\|;:'\"`~<>[]{}";

        var options = ruleBuilder
            .Must(password => allowedSpecialCharacters.Any(password.Contains))
            .WithMessage($"'Password' must contain at least one special character - {allowedSpecialCharacters}");

        return options;
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