using TestPlatform.Services.DTOs.ExamDTOs;

namespace TestPlatform.Services.ExamServices;

public interface IExamService
{
    ValueTask<ExamDto?> CreateAsync(ExamForCreationDto examForCreationDto);
    ValueTask<QuizInExamDto> FirstOrNextQuizAsync(GetQuizInExamDto getQuizInExamDto);
    ValueTask<List<ExamDto>> GetExamsOfUserAsync(long userId);
    ValueTask<List<SolvedQuizDto>> GetSolvedQuizzesOfExamAsync(long examId);
    ValueTask<ResultDto> GetResultExamAsync(long examId);
}