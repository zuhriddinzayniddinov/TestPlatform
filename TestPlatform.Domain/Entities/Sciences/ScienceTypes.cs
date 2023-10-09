using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Domain.Entities.Sciences;

public class ScienceTypes
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public string? PhotoUrl { get; set; }
}