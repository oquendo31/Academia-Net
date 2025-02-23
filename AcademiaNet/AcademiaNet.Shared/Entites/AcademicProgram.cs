using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaNet.Shared.Entites;

public class AcademicProgram
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
    //public ICollection<Enrollment>? Enrollments { get; set; }
    //public int EnrollmentsCount => Enrollments == null ? 0 : Enrollments.Count;
    //public ICollection<PeriodAcademicProgram>? PeriodAcademicProgramss { get; set; }
    //public int PeriodAcademicProgramssCount => PeriodAcademicProgramss == null ? 0 : PeriodAcademicProgramss.Count;
}