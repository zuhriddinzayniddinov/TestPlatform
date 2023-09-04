using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Domain.Entities.Users;

public class User
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(30)]
    [MinLength(4)]
    public string Username { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(8)]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; }
    [Required]
    public DateTime CreateDate { get; } = DateTime.UtcNow;
    [Required]
    public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
    public string PasswordHash { get; set; }
    public string Salt { get; } = Guid.NewGuid().ToString();
    public string Phone { get; set; }
}