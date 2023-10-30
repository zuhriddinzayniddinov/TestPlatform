using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.Domain.Entities.Quizzes;

namespace TestPlatform.Domain.Entities.Exam;

public class QuizInExam
{
    [Key] public long Id { get; set; }
    [ForeignKey(nameof(Exam))] public long ExamId { get; set; }
    [ForeignKey(nameof(Quiz))] public long QuizId { get; set; }
    public int QuizStatus { get; set; } = 0;
    public int Order { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime FinishAt { get; set; }
    public string? Question { get; set; }
    public string? Answer1 { get; set; }
    public string? AnswerGuid1 { get; set; }
    public string? Answer2 { get; set; }
    public string? AnswerGuid2 { get; set; }
    public string? Answer3 { get; set; }
    public string? AnswerGuid3 { get; set; }
    public string? Answer4 { get; set; }
    public string? AnswerGuid4 { get; set; }
    public string? GivenGuid { get; set; }
}