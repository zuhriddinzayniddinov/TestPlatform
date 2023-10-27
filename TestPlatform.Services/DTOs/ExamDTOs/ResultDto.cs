namespace TestPlatform.Services.DTOs.ExamDTOs;

public record ResultDto(
    long id,
    long userId,
    long scienceId,
    string scienceName,
    DateTime createAt,
    DateTime closeAt,
    int count,
    int rightCount);