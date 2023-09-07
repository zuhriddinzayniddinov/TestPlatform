using TestPlatform.Services.DTOs.AuthenticationDTOs;

namespace TestPlatform.Services.Authentication;

public interface IAuthenticationService
{
    ValueTask<TokenDto> LogInAsync(AuthenticationDto authenticationDto);
    ValueTask<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    ValueTask<bool> LogOutAsync(RefreshTokenDto refreshTokenDto);
}