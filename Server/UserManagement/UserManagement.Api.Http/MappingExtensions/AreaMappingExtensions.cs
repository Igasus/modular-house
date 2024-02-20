using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.MappingExtensions;

public static class AreaMappingExtensions
{
    public static AreaDtoInput AsInputDto(this AreaRequest request)
    {
        return new AreaDtoInput { Id = request.Id };
    }

    public static AreaResponse AsResponse(this AreaDto dto)
    {
        return new AreaResponse
        {
            Id = dto.Id,
            AdditionDate = dto.AdditionTime
        };
    }
}