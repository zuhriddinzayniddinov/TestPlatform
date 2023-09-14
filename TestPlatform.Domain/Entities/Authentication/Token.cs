using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Domain.Entities.Authentication;

public class Token
{
    [Key]
    [Required]
    public string DeviceToken { get; set; }
    [Required]
    public long UserId { get; set; }
    public User User { get; set; }
    [Required]
    public string Device { get; set; }
    [Required]
    public DateTime ExpireDate { get; set; }
    [Required]
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
}