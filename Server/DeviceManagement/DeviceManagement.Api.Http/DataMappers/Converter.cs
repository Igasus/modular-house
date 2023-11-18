using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class Converter
{
    public static CreatedUserResponse ToResponse(this User user)
    {
        return new CreatedUserResponse(user.Id, user.AdditionDate);
    }
}