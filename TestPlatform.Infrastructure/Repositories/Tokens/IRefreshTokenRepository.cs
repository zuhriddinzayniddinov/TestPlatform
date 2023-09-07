using TestPlatform.Domain.Entities.Authentication;

namespace TestPlatform.Infrastructure.Repositories.Tokens;

public interface IRefreshTokenRepository
    : IGenericRepository<RefreshToken, string>
{
}