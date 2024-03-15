using System;

namespace ModularHouse.Shared.Models.AuthSystem.Responses;

public record SignUpResponse
{
    public Guid UserId { get; init; }
    public string AccessToken { get; init; }
}