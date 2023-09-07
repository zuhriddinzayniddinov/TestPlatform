namespace TestPlatform.Services.DTOs.AuthenticationDTOs;

public record TokenDto(
    string accessToken,
    string? refreshToken,
    DateTime expireDate);