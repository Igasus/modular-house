using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Auth.Application.AccessToken;
using ModularHouse.Server.Auth.Application.CQRS.Queries;
using ModularHouse.Server.Auth.Application.CQRS.QueryResponses;
using ModularHouse.Server.Auth.Domain;
using ModularHouse.Server.Auth.Domain.UserAggregate;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;

namespace ModularHouse.Server.Auth.Application.CQRS.QueryHandlers;

public class GetAccessTokenQueryHandler(IUserDataSource dataSource, IAccessTokenManager accessTokenManager)
    : IQueryHandler<GetAccessTokenQuery, GetAccessTokenQueryResponse>
{
    public async Task<GetAccessTokenQueryResponse> Handle(
        GetAccessTokenQuery query,
        CancellationToken cancellationToken)
    {
        var user = await dataSource.GetByEmailAsync(query.Credentials.Email, cancellationToken);
        if (user is null)
        {
            throw new BadRequestException(
                LocalErrorMessages.InvalidCredentials,
                ErrorMessages.NotFoundDetails((User u) => u.Email, query.Credentials.Email));
        }

        var isPasswordValid = user.ValidatePassword(query.Credentials.Password);
        if (!isPasswordValid)
            throw new BadRequestException(LocalErrorMessages.InvalidCredentials, LocalErrorMessages.IncorrectPassword);

        var accessToken = accessTokenManager.CreateJsonWebToken(user.Id);

        return new GetAccessTokenQueryResponse(user.Id, accessToken);
    }
}