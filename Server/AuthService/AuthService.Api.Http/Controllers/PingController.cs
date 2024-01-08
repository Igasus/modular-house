using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularHouse.Server.AuthService.Api.Http.Controllers;

/// <summary>
/// Represents the Ping API controller.
/// </summary>
[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    /// <summary>
    /// Ping.
    /// </summary>
    /// <returns>ActionResult.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> PingAsync()
    {
        IActionResult response = Ok();
        return Task.FromResult(response);
    }
}