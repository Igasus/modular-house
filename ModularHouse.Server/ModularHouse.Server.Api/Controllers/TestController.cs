using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularHouse.Server.Application;
using ModularHouse.Server.Domain.UserAggregate;

namespace ModularHouse.Server.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly IUserService _userService;

    public TestController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get List of Users
    /// </summary>
    /// <returns>Task with ActionResult.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }
    
    /// <summary>
    /// CreateUser
    /// </summary>
    /// <returns>Task with ActionResult.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] User input)
    {
        var userId = await _userService.CreateAsync(input);
        var user = await _userService.GetByIdAsync(userId);

        return Ok(user);
    }
}