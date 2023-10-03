using System;

namespace ModularHouse.Shared.Models.Responses.Auth;

public record AuthSignInResponseUser(Guid Id, string UserName);