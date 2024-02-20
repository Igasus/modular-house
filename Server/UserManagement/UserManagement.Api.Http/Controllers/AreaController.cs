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
using ModularHouse.Server.UserManagement.Domain.AreaAggregate.Events;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/areas")]
public class AreaController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    private readonly IDomainEventBus _domainEventBus;

    public AreaController(IMessageBus messageBus, IDomainEventBus domainEventBus)
    {
        _messageBus = messageBus;
        _domainEventBus = domainEventBus;
    }

    /// <summary>
    /// Create Area.
    /// </summary>
    /// <param name="request">Input data.</param>
    /// <returns>Action result with created Area.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AreaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody, Required] AreaRequest request)
    {
        var areaCreatedTask =
            _domainEventBus.WaitAsync<AreaCreatedEvent>(transactionId: CurrentTransaction.TransactionId);

        var input = request.AsInputDto();
        var createAreaCommand = new CreateAreaCommand(input);
        await _messageBus.Send(createAreaCommand);

        await areaCreatedTask;
        var getAreaQuery = new GetAreaByIdQuery(input.Id);
        var getAreaQueryResponse = await _messageBus.Send<GetAreaByIdQuery, GetAreaByIdQueryResponse>(getAreaQuery);

        var response = getAreaQueryResponse.Area.AsResponse();
        return Ok(response);
    }

    /// <summary>
    /// Delete Area by Id.
    /// </summary>
    /// <param name="id">Id of Area to delete.</param>
    /// <returns>Action result.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute, Required] Guid id)
    {
        var areaDeletedTask =
            _domainEventBus.WaitAsync<AreaDeletedEvent>(transactionId: CurrentTransaction.TransactionId);

        var deleteAreaCommand = new DeleteAreaByIdCommand(id);
        await _messageBus.Send(deleteAreaCommand);

        await areaDeletedTask;

        return Ok();
    }
}