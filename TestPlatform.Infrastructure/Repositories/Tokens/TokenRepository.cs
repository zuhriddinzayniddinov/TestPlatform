using TestPlatform.Domain.Entities.Authentication;
using TestPlatform.Infrastructure.Contexts;

namespace TestPlatform.Infrastructure.Repositories.Tokens;

public class TokenRepository
    : GenericRepository<Token,string>,
        ITokenRepository
{
    public TokenRepository(AppDbContext appDbContext)
        : base(appDbContext)
    {
    }
}