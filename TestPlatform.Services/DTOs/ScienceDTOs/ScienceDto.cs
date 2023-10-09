namespace TestPlatform.Services.DTOs.ScienceDTOs;

public record ScienceDto(
    long id,
    long scienceTypeId,
    long userId,
    int countQuizzes,
    string name,
    string? phoroUrl);