using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Application.Queries;
using ModularHouse.Server.UserManagement.Application.QueryResponses;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;

namespace ModularHouse.Server.UserManagement.Application.QueryHandlers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersQueryResponse>
{
    private readonly IUserDataSource _dataSource;

    public GetUsersQueryHandler(IUserDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _dataSource.GetAllAsync();
        var usersAsDtoList = users.Select(user => user.AsDto()).ToList();

        return new GetUsersQueryResponse(usersAsDtoList, users.Count);
    }
}