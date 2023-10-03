using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.Temp.Application.Abstractions;
using ModularHouse.Server.Temp.Application.Auth.Contracts;
using ModularHouse.Server.Temp.Application.Commands;

namespace ModularHouse.Server.Temp.Application.CommandHandlers;

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