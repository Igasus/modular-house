using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Server.Temp.Api.MappingExtensions;
using ModularHouse.Server.Temp.Api.Services.Contracts;
using ModularHouse.Server.Temp.Application.Commands;
using ModularHouse.Server.Temp.Application.Queries;
using ModularHouse.Server.Temp.Domain.UserAggregate;
using ModularHouse.Server.Temp.Domain.UserAggregate.Events;
using ModularHouse.Shared.Models.Requests.Auth;
using ModularHouse.Shared.Models.Responses.Auth;

namespace ModularHouse.Server.Temp.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthApiService _authApiService;

    public AuthController(IMediator mediator, IAuthApiService authApiService)
    {
        _mediator = mediator;
        _authApiService = authApiService;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUpAsync([FromBody] AuthSignUpRequest request)
    {
        var transactionId = Guid.NewGuid();

        var user = await _authApiService.SignUpAsync(transactionId, request.UserName, request.Email, request.Password);

        return Ok(user);
    }

    [HttpPost("sing-in")]
    [ProducesResponseType(typeof(AuthSignInResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInAsync([FromBody] AuthSignInRequest request)
    {
        var query = new GetAuthUserInfoQuery(request.UserNameOrEmail, request.Password);
        var authUserInfo = await _mediator.Send(query);
        
        return Ok(authUserInfo.ToResponse());
    }
}