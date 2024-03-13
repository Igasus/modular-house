using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Auth.Api.Http.MappingExtensions;
using ModularHouse.Server.Auth.Application.CQRS.Commands;
using ModularHouse.Server.Auth.Application.CQRS.Queries;
using ModularHouse.Server.Auth.Application.CQRS.QueryResponses;
using ModularHouse.Server.Auth.Domain.UserAggregate.Events;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Shared.Models.AuthSystem.Requests;
using ModularHouse.Shared.Models.AuthSystem.Responses;
using ModularHouse.Shared.Models.Responses;

namespace ModularHouse.Server.Auth.Api.Http.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/auth")]
public class AuthController(IMessageBus messageBus, IDomainEventBus domainEventBus) : ControllerBase
{
    /// <summary>
    /// Sign Up.
    /// </summary>
    /// <param name="request">Sign Up input data</param>
    /// <returns>ActionResult with Sign Up result data</returns>
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(SignUpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest request)
    {
        var userCredentials = request.AsUserCredentials();
        var signUpUserCommand = new SignUpUserCommand(userCredentials);
        var userCreatedTask =
            domainEventBus.WaitAsync<UserCreatedEvent>(transactionId: CurrentTransaction.TransactionId);

        await messageBus.Send(signUpUserCommand);
        await userCreatedTask;
        
        var getAccessTokenQuery = new GetAccessTokenQuery(userCredentials);
        var getAccessTokenQueryResponse =
            await messageBus.Send<GetAccessTokenQuery, GetAccessTokenQueryResponse>(getAccessTokenQuery);

        var response = new SignInResponse
        {
            UserId = getAccessTokenQueryResponse.UserId,
            AccessToken = getAccessTokenQueryResponse.AccessToken
        };
        return Ok(response);
    }

    /// <summary>
    /// Sign In.
    /// </summary>
    /// <param name="request">Sign In input data</param>
    /// <returns>ActionResult with Sign In result data</returns>
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
    {
        var userCredentials = request.AsUserCredentials();
        var getAccessTokenQuery = new GetAccessTokenQuery(userCredentials);
        var getAccessTokenQueryResponse =
            await messageBus.Send<GetAccessTokenQuery, GetAccessTokenQueryResponse>(getAccessTokenQuery);

        var response = new SignInResponse
        {
            UserId = getAccessTokenQueryResponse.UserId,
            AccessToken = getAccessTokenQueryResponse.AccessToken
        };
        return Ok(response);
    }
}