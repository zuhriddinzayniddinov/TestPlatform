namespace TestPlatform.Services.DTOs.QuizDTOs;

public record SolverDto(
    long scienceId,
    long quizId,
    string answer);