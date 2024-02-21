using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Queries;
using ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.UserAggregate;

namespace ModularHouse.Server.UserManagement.Application.CQRS.QueryHandlers;

public class GetUserByIdQueryHandler(IUserDataSource dataSource)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await dataSource.GetByIdAsync(query.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<User>(),
                ErrorMessages.NotFoundDetails((User u) => u.Id, query.UserId));
        }

        return new GetUserByIdQueryResponse(user.AsDto());
    }
}