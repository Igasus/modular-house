using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Server.Common.Domain.Exceptions;
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
        var user = await _userDataSource.Users.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
        return user ?? throw new NotFoundException($"User was not found with given Id: {query.Id}.");
    }
}