using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.UserManagement.Application.CQRS.Queries;
using ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;

namespace ModularHouse.Server.UserManagement.Application.CQRS.QueryHandlers;

public class GetUsersQueryHandler(IUserDataSource dataSource) : IQueryHandler<GetUsersQuery, GetUsersQueryResponse>
{
    public async Task<GetUsersQueryResponse> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await dataSource.GetAllAsync(cancellationToken);
        var usersAsDtoList = users.Select(user => user.AsDto()).ToList();

        return new GetUsersQueryResponse(usersAsDtoList, users.Count);
    }
}