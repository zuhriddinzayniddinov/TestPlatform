namespace TestPlatform.Services.DTOs.AuthenticationDTOs;

public record RefreshTokenDto(
    string accessToken,
    string refreshToken);