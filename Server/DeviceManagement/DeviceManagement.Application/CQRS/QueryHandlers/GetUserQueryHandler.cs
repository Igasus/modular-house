using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResponse>
{
    private readonly IUserDataSource _userDataSource;

    public GetUserQueryHandler(IUserDataSource userDataSource)
    {
        _userDataSource = userDataSource;
    }

    public async Task<GetUserQueryResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userDataSource.GetByIdAsync(query.UserId, cancellationToken);
        var userDto = user.ToDto();

        return new GetUserQueryResponse(userDto);
    }
}