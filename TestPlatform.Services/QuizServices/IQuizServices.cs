using TestPlatform.Services.DTOs.QuizDTOs;

namespace TestPlatform.Services.QuizServices;

public interface IQuizServices
{
    ValueTask<QuizDto> CreateAsync(QuizForCreationDto quizForCreationDto);
    IQueryable<QuizDto> GetByScienceId(long scienceId);
    ValueTask<QuizDto> RemoveAsyncAsync(long quizId);
}