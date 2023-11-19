using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.UserManagement.Api.Http.MappingExtensions;
using ModularHouse.Server.UserManagement.Application.Queries;
using ModularHouse.Server.UserManagement.Application.QueryResponses;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    private readonly IDomainEventBus _domainEventBus;

    public UserController(IMessageBus messageBus, IDomainEventBus domainEventBus)
    {
        _messageBus = messageBus;
        _domainEventBus = domainEventBus;
    }

    /// <summary>
    /// Get All Users.
    /// </summary>
    /// <returns>ActionResult with List of Users</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ListedResponse<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var queryResponse = await _messageBus.Send<GetUsersQuery, GetUsersQueryResponse>(new GetUsersQuery());
        var usersAsResponseList = queryResponse.Users.Select(dto => dto.AsResponse()).ToList();

        var response = new ListedResponse<UserResponse>(usersAsResponseList, queryResponse.TotalUsersCount);
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
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var queryResponse =
            await _messageBus.Send<GetUserByIdQuery, GetUserByIdQueryResponse>(new GetUserByIdQuery(id));

        var response = queryResponse.User.AsResponse();
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
    public async Task<IActionResult> CreateAsync([FromBody] UserRequest input)
    {
        throw new NotImplementedException();
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
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UserResponse input)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Delete User by Id.
    /// </summary>
    /// <param name="id">Id of User to delete</param>
    /// <returns>ActionResult</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}