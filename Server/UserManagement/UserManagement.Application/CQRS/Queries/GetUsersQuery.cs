using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;

namespace ModularHouse.Server.UserManagement.Application.CQRS.Queries;

public record GetUsersQuery : IQuery<GetUsersQueryResponse>;