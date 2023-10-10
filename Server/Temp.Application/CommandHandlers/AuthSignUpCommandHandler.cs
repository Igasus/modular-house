using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.Temp.Application.Abstractions;
using ModularHouse.Server.Temp.Application.Auth.Contracts;
using ModularHouse.Server.Temp.Application.Commands;
using ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;
using ModularHouse.Server.Temp.Domain.UserAggregate.Events;

namespace ModularHouse.Server.Temp.Application.CommandHandlers;

public class AuthSignUpCommandHandler : ICommandHandler<AuthSignUpCommand>
{
    private readonly IAuthService _authService;
    private readonly IDomainEventBus _domainEventBus;

    public AuthSignUpCommandHandler(IAuthService authService, IDomainEventBus domainEventBus)
    {
        _authService = authService;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(AuthSignUpCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.CreateUserAsync(request.UserName, request.Email, request.Password);
        
        await _domainEventBus.PublishAsync(new UserCreatedEvent(request.TransactionId, user));
    }
}