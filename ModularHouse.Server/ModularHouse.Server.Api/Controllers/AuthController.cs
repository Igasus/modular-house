using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Server.Api.MappingExtensions;
using ModularHouse.Server.Application.Commands;
using ModularHouse.Server.Application.Queries;
using ModularHouse.Shared.Models.Requests.Auth;
using ModularHouse.Shared.Models.Responses.Auth;

namespace ModularHouse.Server.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUpAsync([FromBody] AuthSignUpRequest request)
    {
        var command = new AuthSignUpCommand(request.UserName, request.Email, request.Password);
        await _mediator.Send(command);
        
        return Ok();
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