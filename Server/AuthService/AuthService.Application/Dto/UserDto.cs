using System;

namespace ModularHouse.Server.AuthService.Application.Dto;

public class UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; }
}