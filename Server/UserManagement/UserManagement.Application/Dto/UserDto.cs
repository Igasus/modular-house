using System;

namespace ModularHouse.Server.UserManagement.Application.Dto;

public record UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; }
}