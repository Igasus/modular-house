using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.UserManagement.Application.MappingExtensions;

public static class AreaMappingExtensions
{
    public static Area AsEntity(this AreaDtoInput dto)
    {
        return new Area { Id = dto.Id };
    }

    public static AreaDto AsDto(this Area entity)
    {
        return new AreaDto
        {
            Id = entity.Id,
            AdditionTime = entity.AdditionDate
        };
    }
}