using System;

namespace ModularHouse.Shared.Models.Responses.UMS;

public record UserResponse
{
    public Guid Id { get; init; }
    public string Email { get; init; }
}