using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class Enrollment
{
    [Key]
    public int EnrollmentID { get; set; }

    [Required]
    public DateTime EnrollmentDate { get; set; }

    [Required]
    public int AcademicProgramID { get; set; }

    public AcademicProgram AcademicProgram { get; set; } = null!;

    [Required]
    public int ApplicantId { get; set; }

    public Applicant Applicant { get; set; } = null!;
}