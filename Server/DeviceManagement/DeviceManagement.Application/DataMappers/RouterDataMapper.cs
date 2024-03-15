using System.Collections.Generic;
using System.Linq;
using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class RouterDataMapper
{
    public static RouterDto ToDto(this Router router)
    {
        return new RouterDto
        {
            Id = router.Id,
            AreaId = router.AreaId,
            DeviceId = router.DeviceId,
            Name = router.Name,
            Description = router.Description,
            AdditionDate = router.AdditionDate
        };
    }

    public static IReadOnlyList<RouterDto> ToDtoList(this IReadOnlyList<Router> routers)
    {
        return routers.Select(ToDto).ToList();
    }
}