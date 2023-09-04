using Microsoft.AspNetCore.Mvc;
using TestPlatform.Services.DTOs.UserDTOs;
using TestPlatform.Services.UserServices;

namespace TestPlatform.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SignController : ControllerBase
{
    private readonly IUserService _userService;

    public SignController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public string In([FromBody]UserForAccessDto userForAccessDto)
    {
        return "in";
    }
    [HttpPost]
    public async ValueTask<ActionResult<UserDto>> Up([FromBody]UserForCreationDto userForCreationDto)
    {
        var userDto = await _userService.CreateUserAsync(userForCreationDto);

        return Ok(userDto);
    }
}