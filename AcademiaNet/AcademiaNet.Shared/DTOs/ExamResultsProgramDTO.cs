using AcademiaNet.Shared.Entites;
using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.DTOs;

internal class ExamResultsProgramDTO
{
    [Key]
    public int AcademicProgramID { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    [Required]
    public string Category { get; set; } = null!;

    [Required]
    public int InstitutionID { get; set; }

    public Institution? Institution { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
    public int EnrollmentsCount => Enrollments == null ? 0 : Enrollments.Count;
    public ICollection<PeriodAcademicProgram>? PeriodAcademicProgramss { get; set; }
    public int PeriodAcademicProgramssCount => PeriodAcademicProgramss == null ? 0 : PeriodAcademicProgramss.Count;
}