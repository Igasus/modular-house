using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class UserDataMapper
{
    public static UserCreatedResponse ToResponse(this UserCreatedDto user)
    {
        return new UserCreatedResponse(user.Id, user.AdditionDate);
    }

    public static UserDeletedResponse ToResponse(this UserDeletedDto user)
    {
        return new UserDeletedResponse(user.Id, user.AdditionDate);
    }
}