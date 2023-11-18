using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers;

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
    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(CreatedUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromRoute, Required] Guid id)
    {
        var userCreatedTask = _eventBus.WaitAsync<UserCreatedEvent>();
        await _mediator.Send(new CreateUserCommand(id));

        var createdUserEvent = await userCreatedTask;
        return Ok(createdUserEvent.User.ToResponse());
    }

    /// <summary>
    /// Delete User with given Id.
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>Result status code</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute, Required] Guid id)
    {
        var userDeletedTask = _eventBus.WaitAsync<UserDeletedEvent>();
        await _mediator.Send(new DeleteUserCommand(id));

        await userDeletedTask;
        return Ok();
    }
}