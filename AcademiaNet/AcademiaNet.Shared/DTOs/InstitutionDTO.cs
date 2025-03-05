using AcademiaNet.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaNet.Shared.DTOs;

public class InstitutionDTO
{
    [Key]
    public int InstitutionID { get; set; } // Identificador único de la institución

    [Display(Name = "Institution", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    [Display(Name = "Location", ResourceType = typeof(Literals))]
    public int LocationID { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }

    [Display(Name = "Image", ResourceType = typeof(Literals))]
    public string? Photo { get; set; }
}