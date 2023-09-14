using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TestPlatform.Domain.Entities.Authentication;
using TestPlatform.Domain.Exceptions;

namespace TestPlatform.Domain.Entities.Users;

public class User
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(30)]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(30)]
    [MinLength(2)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(8)]
    public string Email { get; set; }
    [Required]
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    [Required]
    public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
    public string PasswordHash { get; set; }
    public string Salt { get; set; } = Guid.NewGuid().ToString();
    public string? Phone { get; set; }
    public ICollection<Token> Tokens { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}