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
public class UserController(IDomainEventBus eventBus, IMessageBus messageBus) : ControllerBase
{
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
        var userCreatedTask = eventBus.WaitAsync<UserCreatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new CreateUserCommand(id));
        var userCreatedEvent = await userCreatedTask;

        var userQueryResponse = await messageBus.Send<GetUserQuery, GetUserQueryResponse>(
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
        var userDeletedTask = eventBus.WaitAsync<UserDeletedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new DeleteUserCommand(id));
        await userDeletedTask;

        return Ok();
    }
}