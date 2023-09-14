using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Infrastructure.Authentication;

public class JwtTokenHandler : IJwtTokenHandler
{
    private readonly JwtOption jwtOption;

    public JwtTokenHandler(IOptions<JwtOption> options)
    {
        this.jwtOption = options.Value;
    }
    public JwtSecurityToken GenerateAccessToken(User user, string deviceModel)
    {
        var claims = new List<Claim>()
        {
            new Claim(CustomClaimNames.Id, user.Id.ToString()),
            new Claim(CustomClaimNames.Email, user.Email),
            new Claim(CustomClaimNames.FirstName, user.FirstName),
            new Claim(CustomClaimNames.LastName, user.LastName),
            new Claim(CustomClaimNames.Device, deviceModel),
        };

        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.jwtOption.SecretKey));

        var token = new JwtSecurityToken(
            issuer: this.jwtOption.Issuer,
            audience: this.jwtOption.Audience,
            expires: DateTime.UtcNow.AddMinutes(this.jwtOption.ExpirationInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(
                key: authSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];

        using var randomGenerator =
            RandomNumberGenerator.Create();

        randomGenerator.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}