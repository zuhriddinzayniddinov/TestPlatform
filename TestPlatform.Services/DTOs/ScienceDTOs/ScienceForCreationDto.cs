namespace TestPlatform.Services.DTOs.ScienceDTOs;

public record ScienceForCreationDto(
    string name,
    bool isPrivate,
    long scienceTypeId,
    long userId);