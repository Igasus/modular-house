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
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate.Events;
using ModularHouse.Shared.Models.Requests.UMS;
using ModularHouse.Shared.Models.Responses;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.UserManagement.Api.Http.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/modules")]
public class ModuleController(IMessageBus messageBus, IDomainEventBus domainEventBus) : ControllerBase
{
    /// <summary>
    /// Create Module.
    /// </summary>
    /// <param name="request">Input data.</param>
    /// <returns>Action result with created Module.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ModuleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody, Required] ModuleRequest request)
    {
        var moduleCreatedTask =
            domainEventBus.WaitAsync<ModuleCreatedEvent>(transactionId: CurrentTransaction.TransactionId);

        var input = request.AsInputDto();
        var createModuleCommand = new CreateModuleCommand(input);
        await messageBus.Send(createModuleCommand);

        await moduleCreatedTask;
        var getModuleQuery = new GetModuleByIdQuery(input.Id);
        var getModuleQueryResponse =
            await messageBus.Send<GetModuleByIdQuery, GetModuleByIdQueryResponse>(getModuleQuery);

        var response = getModuleQueryResponse.Module.AsResponse();
        return Ok(response);
    }

    /// <summary>
    /// Delete Module by Id.
    /// </summary>
    /// <param name="id">Id of Module to delete.</param>
    /// <returns>Action result.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute, Required] Guid id)
    {
        var moduleDeletedTask =
            domainEventBus.WaitAsync<ModuleDeletedEvent>(transactionId: CurrentTransaction.TransactionId);

        var deleteModuleCommand = new DeleteModuleByIdCommand(id);
        await messageBus.Send(deleteModuleCommand);

        await moduleDeletedTask;

        return Ok();
    }
}