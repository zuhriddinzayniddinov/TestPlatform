namespace TestPlatform.Services.DTOs.UserDTOs;

public record UserForAccessDto(
    string? username,
    string? password);