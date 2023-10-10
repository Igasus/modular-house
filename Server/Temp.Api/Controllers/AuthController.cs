using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Server.Temp.Api.MappingExtensions;
using ModularHouse.Server.Temp.Application.Commands;
using ModularHouse.Server.Temp.Application.Queries;
using ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;
using ModularHouse.Server.Temp.Domain.UserAggregate.Events;
using ModularHouse.Shared.Models.Requests.Auth;
using ModularHouse.Shared.Models.Responses.Auth;

namespace ModularHouse.Server.Temp.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDomainEventBus _domainEventBus;

    public AuthController(IMediator mediator, IDomainEventBus domainEventBus)
    {
        _mediator = mediator;
        _domainEventBus = domainEventBus;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUpAsync([FromBody] AuthSignUpRequest request)
    {
        var transactionId = Guid.NewGuid();
        
        var command = new AuthSignUpCommand(transactionId, request.UserName, request.Email, request.Password);
        var eventWaitTask = _domainEventBus.WaitAsync<UserCreatedEvent>(5000, transactionId);
        
        await _mediator.Send(command);
        var result = await eventWaitTask;

        return result.EventReceived
            ? Ok(result.Event.User)
            : Ok(result);
    }

    [HttpPost("sing-in")]
    [ProducesResponseType(typeof(AuthSignInResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInAsync([FromBody] AuthSignInRequest request)
    {
        var query = new GetAuthUserInfoQuery(request.UserNameOrEmail, request.Password);
        var authUserInfo = await _mediator.Send(query);
        
        return Ok(authUserInfo.ToResponse());
    }
}