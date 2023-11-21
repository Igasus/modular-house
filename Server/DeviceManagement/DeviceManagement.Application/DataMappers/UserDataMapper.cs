using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class UserDataMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto(user.Id, user.AdditionDate);
    }
}