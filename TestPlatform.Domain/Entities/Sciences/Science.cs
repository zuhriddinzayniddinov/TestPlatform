using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPlatform.Domain.Entities.Sciences;

public class Science
{
    [Key] public long Id { get; set; }
    [ForeignKey("ScienceTypes")] public long ScienceTypesId { get; set; }
    public virtual ScienceTypes ScienceType { get; set; }
    [ForeignKey("User")] public long UserId { get; set; }
    public int CountQuizzes { get; set; } = 0;
    public bool isPrivate { get; set; } = false;
    [Required]
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    [Required]
    public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
    public string Name { get; set; }
    public string? PhotoUrl { get; set; }
}