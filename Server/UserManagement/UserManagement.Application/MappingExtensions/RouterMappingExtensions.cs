using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.UserManagement.Application.MappingExtensions;

public static class RouterMappingExtensions
{
    public static Router AsEntity(this RouterDtoInput dto)
    {
        return new Router { Id = dto.Id };
    }

    public static RouterDto AsDto(this Router entity)
    {
        return new RouterDto
        {
            Id = entity.Id,
            AdditionTime = entity.AdditionDate
        };
    }
}