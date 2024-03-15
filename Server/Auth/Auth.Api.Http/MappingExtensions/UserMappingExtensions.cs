using ModularHouse.Server.Auth.Application.Dto;
using ModularHouse.Shared.Models.AuthSystem.Requests;

namespace ModularHouse.Server.Auth.Api.Http.MappingExtensions;

public static class UserMappingExtensions
{
    public static UserCredentialsDto AsUserCredentials(this SignUpRequest signUpRequest)
    {
        return new UserCredentialsDto(signUpRequest.Email, signUpRequest.Password);
    }

    public static UserCredentialsDto AsUserCredentials(this SignInRequest signInRequest)
    {
        return new UserCredentialsDto(signInRequest.Email, signInRequest.Password);
    }
}