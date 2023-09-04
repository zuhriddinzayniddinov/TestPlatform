namespace TestPlatform.Services.DTOs.UserDTOs;

public record UserDto(
    long id,
    string username,
    string email);