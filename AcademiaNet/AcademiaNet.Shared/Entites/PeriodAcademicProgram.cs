using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class PeriodAcademicProgram
{
    [Key]
    public int PeriodAcademicProgramID { get; set; }

    [Required]
    public int AcademicProgramID { get; set; }

    public AcademicProgram AcademicProgram { get; set; } = null!;

    [Required]
    public int EnrollmentPeriodID { get; set; }

    public EnrollmentPeriod EnrollmentPeriod { get; set; } = null!;
}