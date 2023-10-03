using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers;

[ApiController]
[Route("api/device-management")]
[Produces(MediaTypeNames.Application.Json)]
public class PingController : ControllerBase
{
    /// <summary>
    /// Ping
    /// </summary>
    /// <returns>Action result.</returns>
    [HttpGet("/ping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> PingAsync()
    {
        IActionResult response = Ok();
        
        return Task.FromResult(response);
    }
}