namespace ModularHouse.Server.UserManagement.Application.Dto;

public record UserInputDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}