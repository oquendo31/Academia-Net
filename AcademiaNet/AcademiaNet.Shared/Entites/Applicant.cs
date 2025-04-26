using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class Applicant
{
    [Key]
    public int AplicantID { get; set; }

    [Required]
    [MaxLength(100)]
    public string? FullName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string? DocumentNumber { get; set; }

    public ICollection<Enrollment>? Enrollments { get; set; }

    public int EnrollmentCount => Enrollments == null ? 0 : Enrollments.Count;
}