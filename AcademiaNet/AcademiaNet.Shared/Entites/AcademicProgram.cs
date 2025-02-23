using AcademiaNet.Shared.Resources;
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

    [Display(Name = "AcademicProgram", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    [Display(Name = "AcademicProgram", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Category { get; set; } = null!;

    [Required]
    public int InstitutionID { get; set; }

    public Institution? Institution { get; set; }
    //public ICollection<Enrollment>? Enrollments { get; set; }
    //public int EnrollmentsCount => Enrollments == null ? 0 : Enrollments.Count;
    //public ICollection<PeriodAcademicProgram>? PeriodAcademicProgramss { get; set; }
    //public int PeriodAcademicProgramssCount => PeriodAcademicProgramss == null ? 0 : PeriodAcademicProgramss.Count;
}