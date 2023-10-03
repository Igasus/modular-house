using System;

namespace ModularHouse.Shared.Models.Responses.Auth;

public record AuthSignInResponseToken(string Value, DateTimeOffset ExpirationDate);