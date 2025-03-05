using AcademiaNet.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaNet.Shared.Entites;

public class Location
{
    [Key]
    public int LocationID { get; set; }

    [Display(Name = "Location", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    // Relación inversa - Una locación puede tener varias instituciones
    public ICollection<Institution>? Institutions { get; set; }
}