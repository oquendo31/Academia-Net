﻿using AcademiaNet.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class Institution
{
    public int InstitutionID { get; set; }

    [Display(Name = "Institution", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? Location { get; set; } // Permitir null

    [MaxLength(100)]
    public string? Description { get; set; } // Permitir null

    [Display(Name = "Image", ResourceType = typeof(Literals))]
    public string? Photo { get; set; }

    public ICollection<AcademicProgram>? AcademicPrograms { get; set; }
    public int AcademicProgramsCount => AcademicPrograms == null ? 0 : AcademicPrograms.Count;
}