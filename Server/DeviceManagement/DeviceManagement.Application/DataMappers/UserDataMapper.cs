using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class UserDataMapper
{
    public static UserCreatedDto ToCreatedDto(this User user)
    {
        return new UserCreatedDto(user.Id, user.AdditionDate);
    }

    public static UserDeletedDto ToDeletedDto(this User user)
    {
        return new UserDeletedDto(user.Id, user.AdditionDate);
    }
}