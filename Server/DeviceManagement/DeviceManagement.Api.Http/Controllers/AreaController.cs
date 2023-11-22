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
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate.Events;
using ModularHouse.Shared.Models.Requests.DMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers;

[ApiController]
[Route("api/areas")]
[Produces(MediaTypeNames.Application.Json)]
public class AreaController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    private readonly IDomainEventBus _eventBus;

    public AreaController(IMessageBus messageBus, IDomainEventBus eventBus)
    {
        _messageBus = messageBus;
        _eventBus = eventBus;
    }

    /// <summary>
    /// Get All Areas.
    /// </summary>
    /// <returns>Action result with list of Areas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ListedResponse<AreaResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var getAreasQueryResponse = await _messageBus.Send<GetAreasQuery, GetAreasQueryResponse>(new GetAreasQuery());
        var areasAsResponseList = getAreasQueryResponse.Areas.ToResponse();

        var response = new ListedResponse<AreaResponse>(areasAsResponseList, getAreasQueryResponse.Areas.Count);
        return Ok(response);
    }

    /// <summary>
    /// Get Area by Id.
    /// </summary>
    /// <param name="id">Area Id</param>
    /// <returns>Action result with Area</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AreaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid id)
    {
        var getAreaByIdQueryResponse =
            await _messageBus.Send<GetAreaByIdQuery, GetAreaByIdQueryResponse>(new GetAreaByIdQuery(id));

        return Ok(getAreaByIdQueryResponse.Area?.ToResponse());
    }

    /// <summary>
    /// Create Area.
    /// </summary>
    /// <param name="input">Input data of Area to create</param>
    /// <returns>Action result with AreaResponse</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AreaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody, Required] AreaRequest input)
    {
        var areaCreatedTask = _eventBus.WaitAsync<AreaCreatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new CreateAreaCommand(input.ToDto()));

        var areaCreatedEvent = await areaCreatedTask;
        var getAreaByIdQuery = new GetAreaByIdQuery(areaCreatedEvent.AreaId);
        var getAreaByIdResponse = await _messageBus.Send<GetAreaByIdQuery, GetAreaByIdQueryResponse>(getAreaByIdQuery);

        return Ok(getAreaByIdResponse.Area.ToResponse());
    }

    /// <summary>
    /// Update Area.
    /// </summary>
    /// <param name="id">Area Id</param>
    /// <param name="input">Input data of Area to update</param>
    /// <returns>Action result with AreaResponse</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AreaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute, Required] Guid id, [FromBody, Required] AreaRequest input)
    {
        var areaUpdatedTask = _eventBus.WaitAsync<AreaUpdatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new UpdateAreaByIdCommand(id, input.ToDto()));

        var areaUpdatedEvent = await areaUpdatedTask;
        var getAreaByIdQuery = new GetAreaByIdQuery(areaUpdatedEvent.AreaId);
        var getAreaByIdResponse = await _messageBus.Send<GetAreaByIdQuery, GetAreaByIdQueryResponse>(getAreaByIdQuery);

        return Ok(getAreaByIdResponse.Area.ToResponse());
    }

    /// <summary>
    /// Delete Area.
    /// </summary>
    /// <param name="id">Area Id</param>
    /// <returns>Action result with status code</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute, Required] Guid id)
    {
        var areaDeletedTask = _eventBus.WaitAsync<AreaDeletedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new DeleteAreaByIdCommand(id));

        await areaDeletedTask;

        return Ok();
    }
}