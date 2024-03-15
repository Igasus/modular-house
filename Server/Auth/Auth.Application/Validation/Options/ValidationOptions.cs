namespace ModularHouse.Server.Auth.Application.Validation.Options;

public class ValidationOptions
{
    public const string Section = "Validation";
    
    public UserCredentialsValidationOptions UserCredentials { get; set; }
}