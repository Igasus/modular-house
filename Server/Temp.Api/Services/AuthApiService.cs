using System;
using System.Threading.Tasks;
using MediatR;
using ModularHouse.Server.Temp.Api.Services.Contracts;
using ModularHouse.Server.Temp.Application.Commands;
using ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;
using ModularHouse.Server.Temp.Domain.UserAggregate;
using ModularHouse.Server.Temp.Domain.UserAggregate.Events;

namespace ModularHouse.Server.Temp.Api.Services;

public class AuthApiService : IAuthApiService
{
    private readonly IMediator _mediator;
    private readonly IDomainEventBus _domainEventBus;

    public AuthApiService(IMediator mediator, IDomainEventBus domainEventBus)
    {
        _mediator = mediator;
        _domainEventBus = domainEventBus;
    }

    public async Task<User> SignUpAsync(Guid transactionId, string userName, string email, string password)
    {
        var eventWaitTask = _domainEventBus.WaitAsync<UserCreatedEvent>(5000, transactionId);

        var command = new AuthSignUpCommand(transactionId, userName, email, password);
        await _mediator.Send(command);

        var result = await eventWaitTask;

        return result.Event.User;
    }
}