using ModularHouse.Server.Auth.Application.Dto;
using ModularHouse.Shared.Models.AuthSystem.Requests;

namespace ModularHouse.Server.Auth.Api.Http.MappingExtensions;

public static class UserMappingExtensions
{
    public static UserCredentials AsUserCredentials(this SignUpRequest signUpRequest)
    {
        return new UserCredentials(signUpRequest.Email, signUpRequest.Password);
    }

    public static UserCredentials AsUserCredentials(this SignInRequest signInRequest)
    {
        return new UserCredentials(signInRequest.Email, signInRequest.Password);
    }
}