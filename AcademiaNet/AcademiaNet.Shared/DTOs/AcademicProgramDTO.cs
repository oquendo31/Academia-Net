using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaNet.Shared.DTOs;

public class AcademicProgramDTO
{
    [Key]
    public int AcademicProgramID { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    [Required]
    public string Category { get; set; } = null!;

    [Display(Name = "Institution", ResourceType = typeof(Literals))]
    public int InstitutionID { get; set; }
}