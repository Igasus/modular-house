using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers;

[ApiController]
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly IDomainEventBus _eventBus;
    private readonly IMessageBus _messageBus;

    public UserController(IDomainEventBus eventBus, IMessageBus messageBus)
    {
        _eventBus = eventBus;
        _messageBus = messageBus;
    }

    /// <summary>
    /// Create User with given Id.
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>Action result with User</returns>
    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromRoute, Required] Guid id)
    {
        var userCreatedTask = _eventBus.WaitAsync<UserCreatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new CreateUserCommand(id));
        var userCreatedEvent = await userCreatedTask;

        var userQueryResponse = await _messageBus.Send<GetUserQuery, GetUserQueryResponse>(
            new GetUserQuery(userCreatedEvent.UserId));

        return Ok(userQueryResponse.User.ToResponse());
    }

    /// <summary>
    /// Delete User with given Id.
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>Action result with status code</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute, Required] Guid id)
    {
        var userDeletedTask = _eventBus.WaitAsync<UserDeletedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new DeleteUserCommand(id));
        await userDeletedTask;

        return Ok();
    }
}