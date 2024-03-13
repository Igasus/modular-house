using System.Collections.Generic;
using System.Linq;
using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Shared.Models.Requests.DMS;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class RouterDataMapper
{
    public static RouterResponse ToResponse(this RouterDto router)
    {
        return new RouterResponse
        {
            Id = router.Id,
            AreaId = router.AreaId,
            DeviceId = router.DeviceId,
            Name = router.Name,
            Description = router.Description,
            AdditionDate = router.AdditionDate
        };
    }

    public static IReadOnlyList<RouterResponse> ToResponseList(this IReadOnlyList<RouterDto> routers)
    {
        return routers.Select(ToResponse).ToList();
    }

    public static RouterInputDto ToDto(this RouterCreateRequest input)
    {
        return new RouterInputDto { Name = input.Name, Description = input.Description };
    }

    public static RouterInputDto ToDto(this RouterUpdateRequest input)
    {
        return new RouterInputDto { Name = input.Name, Description = input.Description };
    }
}