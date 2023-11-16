using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers.Users;

[ApiController]
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly IDomainEventBus _eventBus;
    private readonly IMediator _mediator;

    public UserController(IDomainEventBus eventBus, IMediator mediator)
    {
        _eventBus = eventBus;
        _mediator = mediator;
    }

    /// <summary>
    /// Create User with given Id.
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>Created User</returns>
    /// <response code = "200">If creation was successful</response>
    /// <response code = "400">If the user already exists in the system</response>
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> CreateAsync([FromRoute, Required] Guid id)
    {
        var userCreatedTask = _eventBus.WaitAsync<UserCreatedEvent>();
        await _mediator.Send(new CreateUserCommand(id));

        var createdUserEvent = await userCreatedTask;
        return Ok(createdUserEvent.User);
    }

    /// <summary>
    /// Delete User with given Id.
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>Result status code</returns>
    /// <response code = "200">If deletion was successful</response>
    /// <response code = "404">If the user was not found in the system</response>
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromRoute, Required] Guid id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return Ok();
    }
}