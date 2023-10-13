using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.Domain.Entities.Sciences;

namespace TestPlatform.Domain.Entities.Quizzes;

public class Quiz
{
    [Key]
    public long id { get; set; }
    [ForeignKey(nameof(Science))]
    public long ScienceId { get; set; }
    public virtual Science Science { get; set; }
    public string Question { get; set; }
    public string CorrectAnswer { get; set; }
    public Guid CorrectGuid { get; set; } = Guid.NewGuid();
    public string Wrong1Answer { get; set; }
    public Guid Wrong1Guid { get; set; } = Guid.NewGuid();
    public string Wrong2Answer { get; set; }
    public Guid Wrong2Guid { get; set; } = Guid.NewGuid();
    public string Wrong3Answer { get; set; }
    public Guid Wrong3Guid { get; set; } = Guid.NewGuid();
    public DateTime CreateAt { get; set; } = DateTime.Now;
}