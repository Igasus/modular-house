using System;

namespace ModularHouse.Shared.Models.Requests.UMS;

public record UserRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}