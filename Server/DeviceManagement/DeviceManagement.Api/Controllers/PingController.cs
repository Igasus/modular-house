using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularHouse.Server.DeviceManagement.Api.Controllers;

[ApiController]
[Route("api/ping")]
[Produces(MediaTypeNames.Application.Json)]
public class PingController : ControllerBase
{
    /// <summary>
    /// Ping
    /// </summary>
    /// <returns>Action result.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> PingAsync()
    {
        IActionResult response = Ok();

        return Task.FromResult(response);
    }
}