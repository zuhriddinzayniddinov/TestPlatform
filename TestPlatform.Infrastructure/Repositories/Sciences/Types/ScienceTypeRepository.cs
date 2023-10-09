using TestPlatform.Domain.Entities.Sciences;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Sciences.Types;

public class ScienceTypeRepository : GenericRepository<ScienceTypes,long>, IScienceTypeRepository
{
    public ScienceTypeRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}