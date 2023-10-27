namespace TestPlatform.Services.DTOs.ExamDTOs;

public record GetQuizInExamDto(
    long? presentQuizId,
    string? answer,
    long examId,
    int presentOrder = 0);