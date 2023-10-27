using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Exams.Exam;

public class ExamRepository : GenericRepository<Domain.Entities.Exam.Exam,long> , IExamRepository
{
    public ExamRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}