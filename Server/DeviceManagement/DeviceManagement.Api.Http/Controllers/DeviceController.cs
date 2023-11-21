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
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate.Events;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers;

[ApiController]
[Route("api/devices")]
[Produces(MediaTypeNames.Application.Json)]
public class DeviceController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    private readonly IDomainEventBus _eventBus;

    public DeviceController(IMessageBus messageBus, IDomainEventBus eventBus)
    {
        _messageBus = messageBus;
        _eventBus = eventBus;
    }

    /// <summary>
    /// Create Device with given Id.
    /// </summary>
    /// <param name="id">Device Id</param>
    /// <returns>Created Device</returns>
    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(DeviceCreatedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromRoute, Required] Guid id)
    {
        var deviceCreatedTask = _eventBus.WaitAsync<DeviceCreatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new CreateDeviceCommand(id));
        var deviceCreatedEvent = await deviceCreatedTask;

        var deviceQueryResponse = 
            await _messageBus.Send<GetDeviceQuery, GetDeviceQueryResponse>(new GetDeviceQuery(deviceCreatedEvent.DeviceId));

        return Ok(deviceQueryResponse.Device.ToCreatedResponse());
    }

    /// <summary>
    /// Delete Device with given Id.
    /// </summary>
    /// <param name="id">Device Id</param>
    /// <returns>Result status code</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute, Required] Guid id)
    {
        var deviceDeletedTask = _eventBus.WaitAsync<DeviceDeletedEvent>(transactionId: CurrentTransaction.TransactionId);
        await _messageBus.Send(new DeleteDeviceCommand(id));
        await deviceDeletedTask;

        return Ok();
    }
}