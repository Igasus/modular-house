namespace ModularHouse.Shared.Models.Responses.Auth;

public record AuthSignInResponse(AuthSignInResponseToken Token, AuthSignInResponseUser User);