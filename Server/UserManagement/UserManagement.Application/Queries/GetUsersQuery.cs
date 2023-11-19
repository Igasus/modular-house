using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.UserManagement.Application.QueryResponses;

namespace ModularHouse.Server.UserManagement.Application.Queries;

public record GetUsersQuery : IQuery<GetUsersQueryResponse>;