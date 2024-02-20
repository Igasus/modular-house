using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;

namespace ModularHouse.Server.UserManagement.Application.MappingExtensions;

public static class ModuleMappingExtensions
{
    public static Module AsEntity(this ModuleDtoInput dto)
    {
        return new Module { Id = dto.Id };
    }

    public static ModuleDto AsDto(this Module entity)
    {
        return new ModuleDto
        {
            Id = entity.Id,
            AdditionTime = entity.AdditionDate
        };
    }
}