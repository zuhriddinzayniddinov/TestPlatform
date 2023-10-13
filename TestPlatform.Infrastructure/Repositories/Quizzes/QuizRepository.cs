using TestPlatform.Domain.Entities.Quizzes;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Quizzes;

public class QuizRepository : GenericRepository<Quiz,long> , IQuizRepository
{
    public QuizRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}