namespace TestPlatform.Services.DTOs.QuizDTOs;

public record QuizDto(
    long id,
    long scienceId,
    string question,
    string answerA,
    string answerGuidA,
    string answerB,
    string answerGuidB,
    string answerC,
    string answerGuidC,
    string answerD,
    string answerGuidD);