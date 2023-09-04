namespace TestPlatform.Services.DTOs.UserDTOs;

public record UserForCreationDto(
    string? username,
    string? password,
    string? email);