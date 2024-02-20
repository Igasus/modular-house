using ModularHouse.Server.UserManagement.Application.Dto;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.MappingExtensions;

public static class ModuleMappingExtensions
{
    public static ModuleDtoInput AsInputDto(this ModuleRequest request)
    {
        return new ModuleDtoInput { Id = request.Id };
    }

    public static ModuleResponse AsResponse(this ModuleDto dto)
    {
        return new ModuleResponse
        {
            Id = dto.Id,
            AdditionDate = dto.AdditionTime
        };
    }
}