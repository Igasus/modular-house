namespace ModularHouse.Shared.Models.Requests.Auth;

public record AuthSignUpRequest(string UserName, string Email, string Password);