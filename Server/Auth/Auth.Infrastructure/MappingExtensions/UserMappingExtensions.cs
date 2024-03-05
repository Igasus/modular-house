using ModularHouse.Server.Auth.Application.Dto;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.Auth.Infrastructure.MappingExtensions;

public static class UserMappingExtensions
{
    public static UserRequest AsRequest(this UserCreateDto userCreateDto)
    {
        return new UserRequest
        {
            Email = userCreateDto.Email,
            Password = userCreateDto.Password
        };
    }

    public static UserDto AsDto(this UserResponse userResponse)
    {
        return new UserDto
        {
            Id = userResponse.Id,
            Email = userResponse.Email
        };
    }
}