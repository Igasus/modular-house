using System.Collections.Generic;
using System.Linq;
using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class ModuleDataMapper
{
    public static ModuleDto ToDto(this Module module)
    {
        return new ModuleDto
        {
            Id = module.Id,
            RouterId = module.RouterId,
            DeviceId = module.DeviceId,
            Name = module.Name,
            Description = module.Description,
            AdditionDate = module.AdditionDate
        };
    }

    public static IReadOnlyList<ModuleDto> ToDtoList(this IReadOnlyList<Module> modules)
    {
        return modules.Select(ToDto).ToList();
    }
}