using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace ModularHouse.Server.UserManagement.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/ping")]
public class PingController : ControllerBase
{
    private readonly ILogger<PingController> _logger;

    public PingController(ILogger<PingController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> PingAsync()
    {
        _logger.LogInformation("Endpoint GET - '../api/ping' has just been requested.");
        
        IActionResult response = Ok();
        return Task.FromResult(response);
    }
}