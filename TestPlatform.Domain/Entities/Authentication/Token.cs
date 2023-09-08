using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Domain.Entities.Authentication;

public class Token
{
    [Key]
    [Required]
    public string DeviceToken { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public string Device { get; set; }
    [Required]
    public DateTime ExpireDate { get; set; }
    [Required]
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
}