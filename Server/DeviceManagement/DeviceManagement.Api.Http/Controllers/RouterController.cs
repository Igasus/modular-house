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
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;
using ModularHouse.Shared.Models.Requests.DMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers;

[ApiController]
[Route("api/routers")]
[Produces(MediaTypeNames.Application.Json)]
public class RouterController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    private readonly IDomainEventBus _eventBus;

    public RouterController(IMessageBus messageBus, IDomainEventBus eventBus)
    {
        _messageBus = messageBus;
        _eventBus = eventBus;
    }

    /// <summary>
    /// Get all routers.
    /// </summary>
    /// <returns>Action result with list of routers</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ListedResponse<RouterResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var getRoutersQueryResponse = 
            await _messageBus.Send<GetRoutersQuery, GetRoutersQueryResponse>(new GetRoutersQuery());

        var routersAsResponseList = getRoutersQueryResponse.Routers.ToResponseList();
        
        var response = 
            new ListedResponse<RouterResponse>(routersAsResponseList, getRoutersQueryResponse.TotalRoutersCount);

        return Ok(response);
    }

    /// <summary>
    /// Get router by Id.
    /// </summary>
    /// <param name="id">Router Id</param>
    /// <returns>Action result with Router</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RouterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid id)
    {
        var getRouterByIdQueryResponse =
            await _messageBus.Send<GetRouterByIdQuery, GetRouterByIdQueryResponse>(new GetRouterByIdQuery(id));

        return Ok(getRouterByIdQueryResponse.Router.ToResponse());
    }

    /// <summary>
    /// Create Router.
    /// </summary>
    /// <param name="input">Input data of Router</param>
    /// <returns>Action result with created Router</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RouterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody, Required] RouterCreateRequest input)
    {
        var routerCreatedTask = _eventBus.WaitAsync<RouterCreatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new CreateRouterCommand(input.ToDto(), input.AreaId, input.DeviceId, input.UserId));

        var routerCreatedEvent = await routerCreatedTask;
        
        var getRouterByIdResponse = await _messageBus.Send<GetRouterByIdQuery, GetRouterByIdQueryResponse>(
            new GetRouterByIdQuery(routerCreatedEvent.RouterId));

        return Ok(getRouterByIdResponse.Router.ToResponse());
    }

    /// <summary>
    /// Update Router.
    /// </summary>
    /// <param name="id">Router Id</param>
    /// <param name="input">Input data of Router</param>
    /// <returns>Action result with updated Router</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RouterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute, Required] Guid id,
        [FromBody, Required] RouterUpdateRequest input)
    {
        var routerUpdatedTask = _eventBus.WaitAsync<RouterUpdatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new UpdateRouterByIdCommand(id, input.UserId, input.ToDto()));
        
        var routerUpdatedEvent = await routerUpdatedTask;
        
        var getRouterByIdResponse = await _messageBus.Send<GetRouterByIdQuery, GetRouterByIdQueryResponse>(
            new GetRouterByIdQuery(routerUpdatedEvent.RouterId));

        return Ok(getRouterByIdResponse.Router.ToResponse());
    }

    /// <summary>
    /// Delete Router.
    /// </summary>
    /// <param name="id">Router Id</param>
    /// <returns>Action result with operation status code</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute, Required] Guid id)
    {
        var routerDeletedTask = _eventBus.WaitAsync<RouterDeletedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new DeleteRouterByIdCommand(id));
        
        await routerDeletedTask;

        return Ok();
    }
}