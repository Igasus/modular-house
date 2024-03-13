using System.Collections.Generic;
using System.Linq;
using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class AreaDataMapper
{
    public static AreaDto ToDto(this Area area)
    {
        return new AreaDto
        {
            Id = area.Id,
            Name = area.Name,
            Description = area.Description,
            AdditionDate = area.AdditionDate
        };
    }

    public static IReadOnlyList<AreaDto> ToDtoList(this IReadOnlyList<Area> areas)
    {
        return areas.Select(ToDto).ToList();
    }
}