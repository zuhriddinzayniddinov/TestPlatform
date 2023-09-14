using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Domain.Entities.Authentication;

public class RefreshToken
{
    [Key]
    [Required]
    public string DeviceRefreshToken { get; set; }
    [Required]
    public long UserId { get; set; }
    public User User { get; set; }
    [Required]
    public string Device { get; set; }
}