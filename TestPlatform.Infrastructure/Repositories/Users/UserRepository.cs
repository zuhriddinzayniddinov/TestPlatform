using Microsoft.EntityFrameworkCore;
using TestPlatform.Domain.Entities.Users;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Users;

public class UserRepository : GenericRepository<User, long> , IUserRepository
{
    public UserRepository(AppDbContext appDbContext)
        : base(appDbContext)
    {
    }

    public async ValueTask<User?> SelectByEmailAsync(string email)
    {
        return await appDbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}