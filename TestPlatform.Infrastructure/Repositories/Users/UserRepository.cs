using TestPlatform.Domain.Entities.Users;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Users;

public class UserRepository : GenericRepository<User, long> , IUserRepository
{
    public UserRepository(AppDbContext appDbContext)
        : base(appDbContext)
    {
    }
}