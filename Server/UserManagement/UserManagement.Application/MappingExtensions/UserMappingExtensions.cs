using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;

namespace ModularHouse.Server.UserManagement.Application.MappingExtensions;

public static class UserMappingExtensions
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}