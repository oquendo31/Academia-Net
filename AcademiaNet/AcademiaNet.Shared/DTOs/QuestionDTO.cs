using AcademiaNet.Shared.Entites;
using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.DTOs;

public class QuestionDTO
{
    [Key]
    public int QuestionID { get; set; }

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = string.Empty; // Texto de la respuesta

    [Required]
    public int ExamID { get; set; }

    public Exam? Exam { get; set; }

    // Relación con preguntas
    public List<AnswerDTO> Answers { get; set; } = new();
}