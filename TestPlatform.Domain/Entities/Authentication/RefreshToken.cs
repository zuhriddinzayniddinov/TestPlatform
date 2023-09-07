using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Domain.Entities.Authentication;

public class RefreshToken
{
    [Key]
    [Required]
    public string DeviceRefreshToken { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public string Device { get; set; }
}