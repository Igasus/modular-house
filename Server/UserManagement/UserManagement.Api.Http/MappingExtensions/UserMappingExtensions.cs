using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.MappingExtensions;

public static class UserMappingExtensions
{
    public static UserResponse AsResponse(this UserDto dto)
    {
        return new UserResponse
        {
            Id = dto.Id,
            Email = dto.Email
        };
    }

    public static UserInputDto AsInputDto(this UserRequest request)
    {
        return new UserInputDto
        {
            Email = request.Email,
            Password = request.Password
        };
    }
}