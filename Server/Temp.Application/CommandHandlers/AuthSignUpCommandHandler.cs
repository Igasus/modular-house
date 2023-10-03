using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.Application.Abstractions;
using ModularHouse.Server.Application.Auth.Contracts;
using ModularHouse.Server.Application.Commands;

namespace ModularHouse.Server.Application.CommandHandlers;

public class AuthSignUpCommandHandler : ICommandHandler<AuthSignUpCommand>
{
    private readonly IAuthService _authService;

    public AuthSignUpCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task Handle(AuthSignUpCommand request, CancellationToken cancellationToken)
    {
        await _authService.CreateUserAsync(request.UserName, request.Email, request.Password);
    }
}