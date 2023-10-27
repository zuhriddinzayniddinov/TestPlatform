using TestPlatform.Domain.Entities.Exam;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Exams.Quiz;

public class QuizInExamRepository : GenericRepository<QuizInExam, long> , IQuizInExamRepository
{
    public QuizInExamRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}