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

    [Display(Name = "AcademicProgram", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    [Display(Name = "Category", ResourceType = typeof(Literals))]
    public int CategoryID { get; set; }

    public string? CategoryName { get; set; }  // Opcional, solo para mostrar el nombre de la categoría (útil para vistas)

    [Range(1, int.MaxValue, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    [Display(Name = "Institution", ResourceType = typeof(Literals))]
    public int InstitutionID { get; set; }
}