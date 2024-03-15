namespace ModularHouse.Server.Auth.Application.Validation.Options;

public class PasswordValidationOptions
{
    public int MinimumLength { get; set; }
    public int MaximumLength { get; set; }
    public bool MustContainUppercaseEnglishLetter { get; set; }
    public bool MustContainLowercaseEnglishLetter { get; set; }
    public bool MustContainDigit { get; set; }
    public bool MustContainSpecialCharacter { get; set; }
    public string AllowedSpecialCharacters { get; set; }
}