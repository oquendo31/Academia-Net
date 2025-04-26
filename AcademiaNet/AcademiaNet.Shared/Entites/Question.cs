using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class Question
{
    [Key]
    public int QuestionID { get; set; } // Identificador único para la respuesta

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = string.Empty; // Texto de la respuesta

    [Required]
    public int ExamID { get; set; }

    public Exam? Exam { get; set; }

    // Relación con preguntas
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}