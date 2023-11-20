using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Application.QueryResponses;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class UserDataMapper
{
    public static GetUserQueryResponse ToResponse(this User user)
    {
        return new GetUserQueryResponse(new GetUserDto(user.Id, user.AdditionDate));
    }

    public static UserCreatedDto ToCreatedDto(this User user)
    {
        return new UserCreatedDto(user.Id, user.AdditionDate);
    }

    public static UserDeletedDto ToDeletedDto(this User user)
    {
        return new UserDeletedDto(user.Id, user.AdditionDate);
    }
}