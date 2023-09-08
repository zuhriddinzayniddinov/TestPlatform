using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Domain.Entities.Authentication;
using TestPlatform.Domain.Entities.Users;
using TestPlatform.Domain.Exceptions;
using TestPlatform.Infrastructure.Authentication;
using TestPlatform.Infrastructure.Repositories.Tokens;
using TestPlatform.Infrastructure.Repositories.Users;
using TestPlatform.Services.DTOs.AuthenticationDTOs;

namespace TestPlatform.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthenticationService(
        IJwtTokenHandler jwtTokenHandler,
        IPasswordHasher passwordHasher,
        IUserRepository userRepository, ITokenRepository tokenRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async ValueTask<TokenDto> LogInAsync(AuthenticationDto authenticationDto)
    {
        var user = await _userRepository.SelectByEmailAsync(authenticationDto.email)
            ?? throw new NotFoundException($"Not Found by {authenticationDto.email} in user");

        if (!_passwordHasher.Verify(user.PasswordHash, authenticationDto.password, user.Salt))
            throw new WrongPasswordException($"user Email: {authenticationDto.email} wrong password: {authenticationDto.password}");

        var accessToken = _jwtTokenHandler.GenerateAccessToken(user,authenticationDto.deviceModel);
        var refreshToken = _jwtTokenHandler.GenerateRefreshToken();

        var token = await _tokenRepository.InsertAsync(new Token()
        {
            Device = authenticationDto.deviceModel,
            UserId = user.Id,
            DeviceToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            ExpireDate = accessToken.ValidTo,
            LastActivity = DateTime.UtcNow
        });

        var refresh = await _refreshTokenRepository.InsertAsync(new RefreshToken()
        {
            Device = authenticationDto.deviceModel,
            DeviceRefreshToken = refreshToken,
            UserId = user.Id
        });

        return new TokenDto(
            accessToken: token.DeviceToken,
            refreshToken: refresh.DeviceRefreshToken,
            expireDate: token.ExpireDate);
    }

    public async ValueTask<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var refreshToken = await _refreshTokenRepository.SelectByIdAsync(refreshTokenDto.refreshToken)
            ?? throw new NotFoundException($"Not Found refresh token {refreshTokenDto.refreshToken}");

        var accessToken = await _tokenRepository.SelectByIdAsync(refreshTokenDto.accessToken)
            ?? throw new NotFoundException($"Not Found access token {refreshTokenDto.accessToken}");

        var user = await _userRepository.SelectByIdAsync(refreshToken.UserId)
                   ?? throw new NotFoundException($"Not Found by RefreshToken {refreshTokenDto.refreshToken} in user");

        var newAccessToken = _jwtTokenHandler.GenerateAccessToken(user,refreshToken.Device);

        var token = await _tokenRepository.InsertAsync(new Token()
        {
            Device = refreshToken.Device,
            UserId = user.Id,
            DeviceToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            ExpireDate = newAccessToken.ValidTo
        });

        return new TokenDto(
            accessToken: token.DeviceToken,
            refreshToken: refreshToken.DeviceRefreshToken,
            expireDate: token.ExpireDate);
    }

    public async ValueTask<bool> LogOutAsync(RefreshTokenDto refreshTokenDto)
    {
        var refreshToken = await _refreshTokenRepository.SelectByIdAsync(refreshTokenDto.refreshToken)
                           ?? throw new NotFoundException($"Not Found refresh token {refreshTokenDto.refreshToken}");

        var accessToken = await _tokenRepository.SelectByIdAsync(refreshTokenDto.accessToken)
                          ?? throw new NotFoundException($"Not Found access token {refreshTokenDto.accessToken}");

        await _refreshTokenRepository.DeleteAsync(refreshToken);

        await _tokenRepository.DeleteAsync(accessToken);

        return true;
    }

    public async ValueTask<int> AllLogOutAsync(RefreshTokenDto refreshTokenDto)
    {
        var token = await _tokenRepository.SelectByIdAsync(refreshTokenDto.accessToken);

        var refreshToken = await _refreshTokenRepository.SelectByIdAsync(refreshTokenDto.refreshToken);

        var user = await _userRepository.SelectByIdAsync(token.UserId);

        var tokens = await _tokenRepository.SelectAll().Where(t => t.UserId == user.Id).ToListAsync();

        var refreshTokens = await _refreshTokenRepository.SelectAll().Where(t => t.UserId == user.Id).ToListAsync();

        foreach (var item in tokens.Where(item => token != item))
        {
            await _tokenRepository.DeleteAsync(item);
        }

        foreach (var item in refreshTokens.Where(item => refreshToken != item))
        {
            await _refreshTokenRepository.DeleteAsync(item);
        }

        return tokens.Count - 1;
    }
}