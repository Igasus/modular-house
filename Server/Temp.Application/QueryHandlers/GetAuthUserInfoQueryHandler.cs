using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.Temp.Application.Auth.Contracts;
using ModularHouse.Server.Temp.Application.Auth.Dto;
using ModularHouse.Server.Temp.Application.Queries;
using ModularHouse.Server.Temp.Domain.Exceptions;
using ModularHouse.Server.Temp.Domain.UserAggregate;
using Shared.InternalMessaging.CQRS.Abstractions;

namespace ModularHouse.Server.Temp.Application.QueryHandlers;

public class GetAuthUserInfoQueryHandler : IQueryHandler<GetAuthUserInfoQuery, AuthUserInfoDto>
{
    private readonly IAuthService _authService;
    private readonly IUserDataSource _userDataSource;
    private readonly IAuthTokenManager _authTokenManager;

    public GetAuthUserInfoQueryHandler(
        IAuthService authService,
        IUserDataSource userDataSource,
        IAuthTokenManager authTokenManager)
    {
        _authService = authService;
        _userDataSource = userDataSource;
        _authTokenManager = authTokenManager;
    }

    public async Task<AuthUserInfoDto> Handle(GetAuthUserInfoQuery request, CancellationToken cancellationToken)
    {
        var emailCredentialsValid =
            await _authService.ValidateCredentialsByEmailAsync(request.UserNameOrEmail, request.Password);

        var userNameCredentialsValid =
            await _authService.ValidateCredentialsByUserNameAsync(request.UserNameOrEmail, request.Password);

        var user = emailCredentialsValid
            ? await _userDataSource.Users
                .FirstOrDefaultAsync(u => u.Email == request.UserNameOrEmail, cancellationToken)
            : userNameCredentialsValid
                ? await _userDataSource.Users
                    .FirstOrDefaultAsync(u => u.UserName == request.UserNameOrEmail, cancellationToken)
                : null;

        if (user is null)
        {
            throw new BadRequestException("Specified credentials are not valid.");
        }

        var authToken = _authTokenManager.GenerateToken(user);

        return new AuthUserInfoDto(authToken, user);
    }
}