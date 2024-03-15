using System;

namespace ModularHouse.Shared.Models.AuthSystem.Responses;

public class SignInResponse
{
    public Guid UserId { get; init; }
    public string AccessToken { get; init; }
}