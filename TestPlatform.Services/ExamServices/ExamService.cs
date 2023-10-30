using Microsoft.EntityFrameworkCore;
using TestPlatform.Domain.Entities.Exam;
using TestPlatform.Infrastructure.Repositories.Exams.Exam;
using TestPlatform.Infrastructure.Repositories.Exams.Quiz;
using TestPlatform.Infrastructure.Repositories.Quizzes;
using TestPlatform.Infrastructure.Repositories.Sciences;
using TestPlatform.Infrastructure.Repositories.Users;
using TestPlatform.Services.DTOs.ExamDTOs;

namespace TestPlatform.Services.ExamServices;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IQuizInExamRepository _quizInExamRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IScienceRepository _scienceRepository;
    private readonly IUserRepository _userRepository;

    public ExamService(IExamRepository examRepository,
        IQuizInExamRepository quizInExamRepository,
        IQuizRepository quizRepository,
        IScienceRepository scienceRepository,
        IUserRepository userRepository)
    {
        _examRepository = examRepository;
        _quizInExamRepository = quizInExamRepository;
        _quizRepository = quizRepository;
        _scienceRepository = scienceRepository;
        _userRepository = userRepository;
    }

    public async ValueTask<ExamDto?> CreateAsync(ExamForCreationDto examForCreationDto)
    {
        var science = await _scienceRepository.SelectByIdAsync(examForCreationDto.scienceId);
        var user = await _userRepository.SelectByIdAsync(examForCreationDto.userId);
        if (science.Id == 0 || user.Id == 0)
            return null;

        var exam = await _examRepository.InsertAsync(new Exam()
        {
            UserId = examForCreationDto.userId,
            ScienceId = examForCreationDto.scienceId
        });

        return new ExamDto(
            exam.Id,
            exam.UserId,
            exam.ScienceId,
            science.Name,
            exam.CreateAt,
            exam.CloseAt);
    }

    public async ValueTask<QuizInExamDto> FirstOrNextQuizAsync(GetQuizInExamDto getQuizInExamDto)
    {
        var exam = await _examRepository.SelectByIdAsync(getQuizInExamDto.examId);

        if (getQuizInExamDto.presentQuizId is not null and not 0)
        {
            var quizInExamOld = await _quizInExamRepository.SelectByIdAsync(getQuizInExamDto.presentQuizId ?? 0);
            quizInExamOld.GivenGuid = getQuizInExamDto.answer;
            quizInExamOld.FinishAt = DateTime.Now;
            var quizOld = await _quizRepository.SelectByIdAsync(quizInExamOld.QuizId);
            if (quizOld.CorrectGuid.ToString() == quizInExamOld.GivenGuid)
                quizInExamOld.QuizStatus = 1;

            quizInExamOld = await _quizInExamRepository.UpdateAsync(quizInExamOld);
            exam.CloseAt = DateTime.Now;
            exam = await _examRepository.UpdateAsync(exam);
        }

        var quiz = await _quizRepository.SelectAll()
            .Where(q => q.ScienceId == exam.ScienceId)
            .Skip(getQuizInExamDto.presentOrder)
            .Take(1)
            .FirstOrDefaultAsync()
            ?? throw new NullReferenceException("Quiz null");

        var answers = new (string,string)[4];

        answers[0] = (quiz.CorrectAnswer, quiz.CorrectGuid.ToString());
        answers[1] = (quiz.Wrong1Answer, quiz.Wrong1Guid.ToString());
        answers[2] = (quiz.Wrong2Answer, quiz.Wrong2Guid.ToString());
        answers[3] = (quiz.Wrong3Answer, quiz.Wrong3Guid.ToString());
        var rand = new Random();

        for (var i = 0; i < 15; i++)
        {
            var index1 = rand.Next(0, 4);
            var index2 = rand.Next(0, 4);
            var temp = answers[index1];
            var temp2 = answers[index2];
            answers[index2] = temp;
            answers[index1] = temp2;
        }

        var quizInExam = new QuizInExam()
        {
            ExamId = exam.Id,
            QuizId = quiz.id,
            Answer1 = answers[0].Item1,
            AnswerGuid1 = answers[0].Item2,
            Answer2 = answers[1].Item1,
            AnswerGuid2 = answers[1].Item2,
            Answer3 = answers[2].Item1,
            AnswerGuid3 = answers[2].Item2,
            Answer4 = answers[3].Item1,
            AnswerGuid4 = answers[3].Item2
        };

        quizInExam = await _quizInExamRepository.InsertAsync(quizInExam);

        return new QuizInExamDto(
            quizInExam.Id,
            quizInExam.QuizId,
            quizInExam.ExamId,
            getQuizInExamDto.presentOrder + 1,
            quiz.Question,
            quizInExam.Answer1,
            quizInExam.AnswerGuid1,
            quizInExam.Answer2,
            quizInExam.AnswerGuid2,
            quizInExam.Answer3,
            quizInExam.AnswerGuid3,
            quizInExam.Answer4,
            quizInExam.AnswerGuid4);
    }

    public async ValueTask<List<ExamDto>> GetExamsOfUserAsync(long userId)
    {
        var exams = await _examRepository
            .SelectAll()
            .Where(e =>
                e.UserId == userId)
            .ToListAsync();

        var sciences = await _scienceRepository.SelectAll().Where(s => exams.Select(e => e.ScienceId).Contains(s.Id))
            .ToDictionaryAsync(s => s.Id);

        return exams.Select(exam => new ExamDto(
                exam.Id,
                exam.UserId,
                exam.ScienceId,
                sciences[exam.ScienceId].Name,
                exam.CreateAt,
                exam.CloseAt))
            .ToList();
    }

    public async ValueTask<List<SolvedQuizDto>> GetSolvedQuizzesOfExamAsync(long examId)
    {
        var exam = await _examRepository.SelectByIdAsync(examId);

        return await _quizInExamRepository.SelectAll()
            .Where(qe => qe.ExamId == exam.Id)
            .Select(q => new SolvedQuizDto(
                q.Id,
                q.QuizId,
                q.ExamId,
                q.Order,
                q.Question,
                q.Answer1,
                q.AnswerGuid1,
                q.Answer2,
                q.AnswerGuid2,
                q.Answer3,
                q.AnswerGuid3,
                q.Answer4,
                q.AnswerGuid4,
                q.GivenGuid,
                q.QuizStatus == 1))
            .ToListAsync();
    }

    public async ValueTask<ResultDto> GetResultExamAsync(long examId)
    {
        var exam = await _examRepository.SelectByIdAsync(examId);

        var science = await _scienceRepository.SelectByIdAsync(exam.ScienceId);

        var quizzesInExam = await _quizInExamRepository.SelectAll()
            .Where(qe => qe.ExamId == exam.Id)
            .ToListAsync();

        return new ResultDto(
            exam.Id,
            exam.UserId,
            exam.ScienceId,
            science.Name,
            exam.CreateAt,
            exam.CloseAt,
            quizzesInExam.Count,
            quizzesInExam.Sum(q => q.QuizStatus));
    }
}