namespace ModularHouse.Shared.Models.AuthSystem.Requests;

public record SignInRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}