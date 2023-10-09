using TestPlatform.Domain.Entities.Sciences;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Sciences;

public class ScienceRepository : GenericRepository<Science, long>, IScienceRepository
{
    public ScienceRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}