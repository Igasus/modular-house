using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class UserDataMapper
{
    public static UserCreatedResponse ToCreatedResponse(this GetUserQueryResponse queryResponse)
    {
        return new UserCreatedResponse(queryResponse.User.Id, queryResponse.User.AdditionDate);
    }
}