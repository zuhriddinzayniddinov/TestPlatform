namespace TestPlatform.Services.DTOs.UserDTOs;

public record UserForCreationDto(
    string? firstname,
    string? lastname,
    string? password,
    string? email);