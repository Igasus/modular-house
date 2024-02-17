using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.UserManagement.Application.Dto;

namespace ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;

public record GetModuleByIdQueryResponse(ModuleDto Module) : IQueryResponse;