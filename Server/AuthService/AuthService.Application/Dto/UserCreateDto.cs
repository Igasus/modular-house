namespace ModularHouse.Server.AuthService.Application.Dto;

public record UserCreateDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}