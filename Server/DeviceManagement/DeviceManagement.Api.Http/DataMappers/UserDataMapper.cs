using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class UserDataMapper
{
    public static UserCreatedResponse ToCreatedResponse(this UserDto user)
    {
        return new UserCreatedResponse(user.Id, user.AdditionDate);
    }
}