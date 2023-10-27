namespace TestPlatform.Services.DTOs.ExamDTOs;

public record QuizInExamDto(
    long id,
    long quizId,
    long examId,
    int order,
    string question,
    string answerA,
    string answerGuidA,
    string answerB,
    string answerGuidB,
    string answerC,
    string answerGuidC,
    string answerD,
    string answerGuidD);