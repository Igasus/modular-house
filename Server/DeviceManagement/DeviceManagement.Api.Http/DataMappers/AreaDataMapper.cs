using System.Collections.Generic;
using System.Linq;
using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Shared.Models.Requests.DMS;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class AreaDataMapper
{
    public static AreaResponse ToResponse(this AreaDto area)
    {
        return new AreaResponse
        {
            Id = area.Id,
            Name = area.Name,
            Description = area.Description,
            CreationDate = area.CreationDate,
            CreatedByUserId = area.CreatedByUserId,
            LastUpdatedDate = area.LastUpdatedDate,
            LastUpdatedByUserId = area.LastUpdatedByUserId
        };
    }

    public static IReadOnlyList<AreaResponse> ToResponseList(this IReadOnlyList<AreaDto> areas)
    {
        return areas.Select(ToResponse).ToList();
    }

    public static AreaInputDto ToDto(this AreaRequest area)
    {
        return new AreaInputDto { Name = area.Name, Description = area.Description, UserId = area.UserId };
    }
}