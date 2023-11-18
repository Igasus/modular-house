using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
    private readonly IUserDataSource _userDataSource;

    public GetUserQueryHandler(IUserDataSource userDataSource)
    {
        _userDataSource = userDataSource;
    }

    public async Task<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        return await _userDataSource.GetByIdAsync(query.Id, cancellationToken);
    }
}