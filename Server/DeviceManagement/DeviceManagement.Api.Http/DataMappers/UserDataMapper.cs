using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class UserDataMapper
{
    public static UserResponse ToResponse(this UserDto user)
    {
        return new UserResponse(user.Id, user.AdditionDate);
    }
}