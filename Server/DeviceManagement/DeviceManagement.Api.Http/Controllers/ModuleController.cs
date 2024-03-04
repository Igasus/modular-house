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
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate.Events;
using ModularHouse.Shared.Models.Requests.DMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers;

[ApiController]
[Route("api/modules")]
[Produces(MediaTypeNames.Application.Json)]
public class ModuleController(IMessageBus messageBus, IDomainEventBus eventBus) : ControllerBase
{
    /// <summary>
    /// Get all modules.
    /// </summary>
    /// <returns>Action result with list of modules</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ListedResponse<ModuleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var getModulesQueryResponse =
            await messageBus.Send<GetModulesQuery, GetModulesQueryResponse>(new GetModulesQuery());

        var response = new ListedResponse<ModuleResponse>(
            getModulesQueryResponse.Modules.ToResponseList(), getModulesQueryResponse.TotalModulesCount);

        return Ok(response);
    }

    /// <summary>
    /// Get Module by id.
    /// </summary>
    /// <param name="id">Module id</param>
    /// <returns>Action result with Module</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ModuleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid id)
    {
        var getModuleByIdQueryResponse =
            await messageBus.Send<GetModuleByIdQuery, GetModuleByIdQueryResponse>(new GetModuleByIdQuery(id));

        return Ok(getModuleByIdQueryResponse.Module.ToResponse());
    }

    /// <summary>
    /// Create Module.
    /// </summary>
    /// <param name="input">Input data of Module</param>
    /// <returns>Action result with created Module</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ModuleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody, Required] ModuleCreateRequest input)
    {
        var moduleCreatedTask = eventBus.WaitAsync<ModuleCreatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new CreateModuleCommand(input.ToDto(), input.RouterId, input.DeviceId));

        var moduleCreatedEvent = await moduleCreatedTask;

        var getModuleByIdQueryResponse = await messageBus.Send<GetModuleByIdQuery, GetModuleByIdQueryResponse>(
            new GetModuleByIdQuery(moduleCreatedEvent.ModuleId));

        return Ok(getModuleByIdQueryResponse.Module.ToResponse());
    }

    /// <summary>
    /// Update Module.
    /// </summary>
    /// <param name="id">Module Id</param>
    /// <param name="input">Input data of Module</param>
    /// <returns>Action result with updated Module</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ModuleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute, Required] Guid id,
        [FromBody, Required] ModuleUpdateRequest input)
    {
        var moduleUpdatedTask = eventBus.WaitAsync<ModuleUpdatedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new UpdateModuleByIdCommand(id, input.ToDto()));

        var moduleUpdatedEvent = await moduleUpdatedTask;

        var getModuleByIdQueryResponse = await messageBus.Send<GetModuleByIdQuery, GetModuleByIdQueryResponse>(
            new GetModuleByIdQuery(moduleUpdatedEvent.ModuleId));

        return Ok(getModuleByIdQueryResponse.Module.ToResponse());
    }

    /// <summary>
    /// Delete Module.
    /// </summary>
    /// <param name="id">Module Id</param>
    /// <returns>Action result with operation status code</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute, Required] Guid id)
    {
        var moduleDeletedTask = eventBus.WaitAsync<ModuleDeletedEvent>(transactionId: CurrentTransaction.TransactionId);
        await messageBus.Send(new DeleteModuleByIdCommand(id));

        await moduleDeletedTask;

        return Ok();
    }
}