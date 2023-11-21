using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class UserDataMapper
{
    public static UserCreatedResponse ToCreatedResponse(this UserCreatedDto user)
    {
        return new UserCreatedResponse(user.Id, user.AdditionDate);
    }
}