using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class Exam
{
    [Key]
    public int ExamID { get; set; }

    [MaxLength(255)]
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    [Range(1, 100)]
    public int MinValue { get; set; } = 1;

    [Required]
    [Range(1, 100)]
    public int MaxValue { get; set; } = 100;

    [Required]
    public int EnrollmentPeriodID { get; set; }

    public EnrollmentPeriod? EnrollmentPeriod { get; set; }

    // Relación con preguntas
    public ICollection<Question> Questions { get; set; } = new List<Question>();

    public ICollection<ExamResult>? ExamResults { get; set; }
    public int ExamResultsCount => ExamResults == null ? 0 : ExamResults.Count;
}