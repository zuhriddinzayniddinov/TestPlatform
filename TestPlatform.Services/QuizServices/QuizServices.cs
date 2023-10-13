using TestPlatform.Domain.Entities.Quizzes;
using TestPlatform.Infrastructure.Repositories.Quizzes;
using TestPlatform.Infrastructure.Repositories.Sciences;
using TestPlatform.Services.DTOs.QuizDTOs;

namespace TestPlatform.Services.QuizServices;

public class QuizServices : IQuizServices
{
    private readonly IQuizRepository _quizRepository;
    private readonly IScienceRepository _scienceRepository;

    public QuizServices(IQuizRepository quizRepository,
        IScienceRepository scienceRepository)
    {
        _quizRepository = quizRepository;
        _scienceRepository = scienceRepository;
    }

    public async ValueTask<QuizDto> CreateAsync(QuizForCreationDto quizForCreationDto)
    {
        var quiz = new Quiz()
        {
            ScienceId = quizForCreationDto.ScienceId,
            Question = quizForCreationDto.question,
            CorrectAnswer = quizForCreationDto.CorrectAnswer,
            Wrong1Answer = quizForCreationDto.answer1,
            Wrong2Answer = quizForCreationDto.answer2,
            Wrong3Answer = quizForCreationDto.answer3
        };

        quiz = await _quizRepository.InsertAsync(quiz);

        var science = await _scienceRepository.SelectByIdAsync(quiz.ScienceId);
        science.CountQuizzes++;
        science = await _scienceRepository.UpdateAsync(science);
        
        return new QuizDto(
            quiz.id,
            quiz.ScienceId,
            quiz.Question,
            quiz.CorrectAnswer,quiz.CorrectGuid.ToString(),
            quiz.Wrong1Answer,quiz.Wrong1Guid.ToString(),
            quiz.Wrong2Answer,quiz.Wrong2Guid.ToString(),
            quiz.Wrong3Answer,quiz.Wrong3Guid.ToString());
    }

    public IQueryable<QuizDto> GetByScienceId(long scienceId)
    {
        return _quizRepository.SelectAll()
            .Where(q => q.ScienceId == scienceId)
            .Select(q => 
                new QuizDto(
                    q.id,
                    q.ScienceId,
                    q.Question,
                    q.CorrectAnswer,q.CorrectGuid.ToString(),
                    q.Wrong1Answer,q.Wrong1Guid.ToString(),
                    q.Wrong2Answer,q.Wrong2Guid.ToString(),
                    q.Wrong3Answer,q.Wrong3Guid.ToString()));
    }

    public async ValueTask<QuizDto> RemoveAsyncAsync(long quizId)
    {
        var quiz = await _quizRepository.SelectByIdAsync(quizId);
        quiz = await _quizRepository.DeleteAsync(quiz);
        
        var science = await _scienceRepository.SelectByIdAsync(quiz.ScienceId);
        science.CountQuizzes--;
        science = await _scienceRepository.UpdateAsync(science);
        
        return new QuizDto(
            quiz.id,
            quiz.ScienceId,
            quiz.Question,
            quiz.CorrectAnswer,quiz.CorrectGuid.ToString(),
            quiz.Wrong1Answer,quiz.Wrong1Guid.ToString(),
            quiz.Wrong2Answer,quiz.Wrong2Guid.ToString(),
            quiz.Wrong3Answer,quiz.Wrong3Guid.ToString());
    }
}