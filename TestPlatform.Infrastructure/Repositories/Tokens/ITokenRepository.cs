using TestPlatform.Domain.Entities.Authentication;

namespace TestPlatform.Infrastructure.Repositories.Tokens;

public interface ITokenRepository
    : IGenericRepository<Token,string>
{
}