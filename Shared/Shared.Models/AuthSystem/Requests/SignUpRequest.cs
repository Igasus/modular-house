namespace ModularHouse.Shared.Models.AuthSystem.Requests;

public record SignUpRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}