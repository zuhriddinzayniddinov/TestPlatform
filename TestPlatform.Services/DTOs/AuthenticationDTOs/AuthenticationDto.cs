namespace TestPlatform.Services.DTOs.AuthenticationDTOs;

public record AuthenticationDto(
    string email,
    string password,
    string deviceModel);