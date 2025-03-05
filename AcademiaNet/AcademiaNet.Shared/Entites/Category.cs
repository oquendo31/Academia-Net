using AcademiaNet.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaNet.Shared.Entites;

public class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Display(Name = "Category", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    // Relación con programas académicos (opcional si quieres navegación inversa)
    public ICollection<AcademicProgram>? AcademicPrograms { get; set; }
}