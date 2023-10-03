namespace ModularHouse.Shared.Models.Requests.Auth;

public record AuthSignInRequest(string UserNameOrEmail, string Password);