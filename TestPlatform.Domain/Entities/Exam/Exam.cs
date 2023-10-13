using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.Domain.Entities.Sciences;
using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Domain.Entities.Exam;

public class Exam
{
    [Key] public long Id { get; set; }
    [ForeignKey(nameof(User))] public long UserId { get; set; }
    [ForeignKey(nameof(Science))] public long ScienceId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime CloseAt { get; set; }
}