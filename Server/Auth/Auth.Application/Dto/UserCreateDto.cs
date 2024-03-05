namespace ModularHouse.Server.Auth.Application.Dto;

public record UserCreateDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}