using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPlatform.Services.DTOs.UserDTOs;
using TestPlatform.Services.Models;
using TestPlatform.Services.UserServices;

namespace TestPlatform.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(
        IUserService userService)
    {
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async ValueTask<ActionResult<UserDto>> PostUserAsync(
        UserForCreationDto userForCreationDto)
    {
        var createdUser = await this.userService
            .CreateUserAsync(userForCreationDto);

        return Created("", createdUser);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetUsers(
        [FromQuery] QueryParameter queryParameter)
    {
        var users = this.userService
            .RetrieveUsers(queryParameter);

        return Ok(users);
    }

    [HttpGet("{userId:long}")]
    public async ValueTask<ActionResult<UserDto>> GetUserByIdAsync(
        long userId)
    {
        var user = await this.userService
            .RetrieveUserByIdAsync(userId);

        return Ok(user);
    }

    [HttpDelete("{userId:long}")]
    public async ValueTask<ActionResult<UserDto>> DeleteUserAsync(
        long userId)
    {
        var removed = await this.userService
            .RemoveUserAsync(userId);

        return Ok(removed);
    }
}