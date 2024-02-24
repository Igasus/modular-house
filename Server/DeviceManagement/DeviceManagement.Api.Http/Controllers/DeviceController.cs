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
public class DeviceController(IMessageBus messageBus, IDomainEventBus eventBus) : ControllerBase
{
    /// <summary>
    /// Create Device with given Id.
    /// </summary>
    /// <param name="id">Device Id</param>
    /// <returns>Action result with Device</returns>
    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromRoute, Required] Guid id)
    {
        var deviceCreatedTask =
            eventBus.WaitAsync<DeviceCreatedEvent>(transactionId: CurrentTransaction.TransactionId);

        await messageBus.Send(new CreateDeviceCommand(id));
        var deviceCreatedEvent = await deviceCreatedTask;

        var deviceQueryResponse = await messageBus.Send<GetDeviceQuery, GetDeviceQueryResponse>(
            new GetDeviceQuery(deviceCreatedEvent.DeviceId));

        return Ok(deviceQueryResponse.Device.ToResponse());
    }

    /// <summary>
    /// Delete Device with given Id.
    /// </summary>
    /// <param name="id">Device Id</param>
    /// <returns>Action result with status code</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute, Required] Guid id)
    {
        var deviceDeletedTask =
            eventBus.WaitAsync<DeviceDeletedEvent>(transactionId: CurrentTransaction.TransactionId);

        await messageBus.Send(new DeleteDeviceCommand(id));
        await deviceDeletedTask;

        return Ok();
    }
}