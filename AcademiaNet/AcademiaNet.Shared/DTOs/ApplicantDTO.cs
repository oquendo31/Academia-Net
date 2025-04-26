using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.DTOs;

internal class ApplicantDTO
{
    [Required]
    public int ApplicantID { get; set; }

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string DocumentNumber { get; set; } = string.Empty;
}