namespace TestPlatform.Services.DTOs.QuizDTOs;

public record AnswerDto(
    bool correctness,
    string answer);