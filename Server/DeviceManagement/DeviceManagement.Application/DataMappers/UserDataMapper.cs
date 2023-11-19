using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Application.QueryResponses;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class UserDataMapper
{
    public static GetUserQueryResponse ToResponse(this User user)
    {
        return new GetUserQueryResponse(new UserDto(user.Id, user.AdditionDate));
    }
}