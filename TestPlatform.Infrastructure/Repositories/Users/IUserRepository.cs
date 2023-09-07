using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Infrastructure.Repositories.Users;

public interface IUserRepository : IGenericRepository<User, long>
{
    ValueTask<User?> SelectByEmailAsync(string email);
}