using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.MappingExtensions;

public static class RouterMappingExtensions
{
    public static RouterDtoInput AsInputDto(this RouterRequest request)
    {
        return new RouterDtoInput { Id = request.Id };
    }

    public static RouterResponse AsResponse(this RouterDto dto)
    {
        return new RouterResponse
        {
            Id = dto.Id,
            AdditionDate = dto.AdditionTime
        };
    }
}