using TestPlatform.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;

namespace TestPlatform.Infrastructure.Authentication;

public interface IJwtTokenHandler
{
    JwtSecurityToken GenerateAccessToken(User user,string deviceModel);
    string GenerateRefreshToken();
}