using System.Collections.Generic;
using System.Linq;
using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Shared.Models.Requests.DMS;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class ModuleDataMapper
{
    public static ModuleResponse ToResponse(this ModuleDto module)
    {
        return new ModuleResponse
        {
            Id = module.Id,
            RouterId = module.RouterId,
            DeviceId = module.DeviceId,
            Name = module.Name,
            Description = module.Description,
            CreationDate = module.CreationDate,
            CreatedByUserId = module.CreatedByUserId,
            LastUpdatedDate = module.LastUpdatedDate,
            LastUpdatedByUserId = module.LastUpdatedByUserId
        };
    }
    
    public static IReadOnlyList<ModuleResponse> ToResponseList(this IReadOnlyList<ModuleDto> modules)
    {
        return modules.Select(ToResponse).ToList();
    }

    public static ModuleInputDto ToDto(this ModuleCreateRequest input)
    {
        return new ModuleInputDto { Name = input.Name, Description = input.Description };
    }

    public static ModuleInputDto ToDto(this ModuleUpdateRequest input)
    {
        return new ModuleInputDto { Name = input.Name, Description = input.Description };
    }
}