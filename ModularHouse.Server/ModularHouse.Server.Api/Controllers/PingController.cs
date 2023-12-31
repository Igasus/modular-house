﻿using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularHouse.Server.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/ping")]
public class PingController : ControllerBase
{
    /// <summary>
    /// Ping
    /// </summary>
    /// <returns>Task with ActionResult.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> PingAsync()
    {
        IActionResult response = Ok();

        return Task.FromResult(response);
    }
}