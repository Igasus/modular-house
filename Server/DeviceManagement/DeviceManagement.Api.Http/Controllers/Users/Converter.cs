using ModularHouse.Server.DeviceManagement.Api.Http.Controllers.Users.Responses;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers.Users;

public static class Converter
{
    public static CreatedUserResponse ToResponse(this User user)
    {
        return new CreatedUserResponse(user.Id, user.AdditionDate);
    }
}