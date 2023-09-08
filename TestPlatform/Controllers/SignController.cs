using Microsoft.AspNetCore.Mvc;
using TestPlatform.API.CustomAuthorize;
using TestPlatform.Services.DTOs.AuthenticationDTOs;

namespace TestPlatform.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SignController : ControllerBase
{
    private readonly Services.Authentication.IAuthenticationService _authenticationService;

    public SignController(Services.Authentication.IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async ValueTask<ActionResult<TokenDto>> In([FromBody]AuthenticationDto authenticationDto)
    {
        return Ok(await _authenticationService.LogInAsync(authenticationDto));
    }
    [HttpPost]
    public async ValueTask<ActionResult<TokenDto>> RefreshToken([FromBody]RefreshTokenDto refreshTokenDto)
    {
        return Ok(await _authenticationService.RefreshTokenAsync(refreshTokenDto));
    }
    [CustomAuthorize]
    [HttpDelete]
    public async ValueTask<ActionResult> Out([FromBody]RefreshTokenDto refreshTokenDto)
    {
        return Ok(await _authenticationService.LogOutAsync(refreshTokenDto));
    }
    [CustomAuthorize]
    [HttpDelete]
    public async ValueTask<ActionResult<int>> AllOut([FromBody] RefreshTokenDto refreshTokenDto)
    {
        return Ok(await _authenticationService.AllLogOutAsync(refreshTokenDto));
    }
}