namespace TestPlatform.Services.DTOs.UserDTOs;

public record UserDto(
    long id,
    string firstname,
    string lastname,
    string email);