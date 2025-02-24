using AcademiaNet.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaNet.Shared.DTOs;

internal class InstitutionDTO
{
    [Key]
    public int InstitutionID { get; set; } // Identificador único de la institución

    [Display(Name = "Institution", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? Location { get; set; } // Ubicación de la institución (opcional)

    [MaxLength(100)]
    public string? Description { get; set; }

    [Display(Name = "Image", ResourceType = typeof(Literals))]
    public string? Photo { get; set; }
}