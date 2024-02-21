using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.UserManagement.Api.Http.MappingExtensions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Application.CQRS.Queries;
using ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.UserManagement.Domain.UserAggregate.Events;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/users")]
public class UserController(IMessageBus messageBus, IDomainEventBus domainEventBus) : ControllerBase
{
    /// <summary>
    /// Get All Users.
    /// </summary>
    /// <returns>ActionResult with List of Users</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ListedResponse<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var getUserQueryResponse = await messageBus.Send<GetUsersQuery, GetUsersQueryResponse>(new GetUsersQuery());
        var usersAsResponseList = getUserQueryResponse.Users.Select(dto => dto.AsResponse()).ToList();

        var response = new ListedResponse<UserResponse>(usersAsResponseList, getUserQueryResponse.TotalUsersCount);
        return Ok(response);
    }

    /// <summary>
    /// Get User by Id.
    /// </summary>
    /// <param name="id">Id of User</param>
    /// <returns>ActionResult with User</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid id)
    {
        var getUserQueryResponse =
            await messageBus.Send<GetUserByIdQuery, GetUserByIdQueryResponse>(new GetUserByIdQuery(id));

        var response = getUserQueryResponse.User.AsResponse();
        return Ok(response);
    }

    /// <summary>
    /// Create User.
    /// </summary>
    /// <param name="input">Input data of User to create</param>
    /// <returns>ActionResult with created User</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody, Required] UserRequest input)
    {
        var userCreatedTask =
            domainEventBus.WaitAsync<UserCreatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new CreateUserCommand(input.AsInputDto()));

        var userCreatedEvent = await userCreatedTask;
        var getUserQueryResponse = await messageBus.Send<GetUserByIdQuery, GetUserByIdQueryResponse>(
            new GetUserByIdQuery(userCreatedEvent.UserId));

        var response = getUserQueryResponse.User.AsResponse();
        return Ok(response);
    }

    /// <summary>
    /// Update User.
    /// </summary>
    /// <param name="id">Id of User to update</param>
    /// <param name="input">Input data of User to update</param>
    /// <returns>ActionResult with updated User</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute, Required] Guid id, [FromBody, Required] UserRequest input)
    {
        var userUpdatedTask =
            domainEventBus.WaitAsync<UserUpdatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new UpdateUserCommand(id, input.AsInputDto()));

        await userUpdatedTask;
        var getUserQueryResponse =
            await messageBus.Send<GetUserByIdQuery, GetUserByIdQueryResponse>(new GetUserByIdQuery(id));

        var response = getUserQueryResponse.User.AsResponse();
        return Ok(response);
    }

    /// <summary>
    /// Delete User by Id.
    /// </summary>
    /// <param name="id">Id of User to delete</param>
    /// <returns>ActionResult</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute, Required] Guid id)
    {
        var userDeletedTask =
            domainEventBus.WaitAsync<UserDeletedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new DeleteUserByIdCommand(id));
        
        await userDeletedTask;
        return Ok();
    }
}