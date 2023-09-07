using TestPlatform.Domain.Entities.Authentication;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Tokens;

public class RefreshTokenRepository
    : GenericRepository<RefreshToken,string>,
        IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext appDbContext)
        : base(appDbContext)
    {
    }
}