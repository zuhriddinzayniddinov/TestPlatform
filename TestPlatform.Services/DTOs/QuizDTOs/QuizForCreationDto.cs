namespace TestPlatform.Services.DTOs.QuizDTOs;

public record QuizForCreationDto(
    long ScienceId,
    string question,
    string CorrectAnswer,
    string answer1,
    string answer2,
    string answer3);