using System;
using System.ComponentModel.DataAnnotations;
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
using ModularHouse.Server.UserManagement.Domain.RouterAggregate.Events;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/routers")]
public class RouterController(IMessageBus messageBus, IDomainEventBus domainEventBus) : ControllerBase
{
    /// <summary>
    /// Create Router.
    /// </summary>
    /// <param name="request">Input data.</param>
    /// <returns>Action result with created Router.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RouterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody, Required] RouterRequest request)
    {
        var routerCreatedTask =
            domainEventBus.WaitAsync<RouterCreatedEvent>(transactionId: CurrentTransaction.TransactionId);

        var input = request.AsInputDto();
        var createRouterCommand = new CreateRouterCommand(input);
        await messageBus.Send(createRouterCommand);

        await routerCreatedTask;
        var getRouterQuery = new GetRouterByIdQuery(input.Id);
        var getRouterQueryResponse =
            await messageBus.Send<GetRouterByIdQuery, GetRouterByIdQueryResponse>(getRouterQuery);

        var response = getRouterQueryResponse.Router.AsResponse();
        return Ok(response);
    }

    /// <summary>
    /// Delete Router by Id.
    /// </summary>
    /// <param name="id">Id of Router to delete.</param>
    /// <returns>Action result.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute, Required] Guid id)
    {
        var routerDeletedTask =
            domainEventBus.WaitAsync<RouterDeletedEvent>(transactionId: CurrentTransaction.TransactionId);

        var deleteRouterCommand = new DeleteRouterByIdCommand(id);
        await messageBus.Send(deleteRouterCommand);

        await routerDeletedTask;

        return Ok();
    }
}