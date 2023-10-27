namespace TestPlatform.Services.DTOs.ExamDTOs;

public record ExamDto(
    long id,
    long userId,
    long scienceId,
    string scienceName,
    DateTime createAt,
    DateTime closeAt);