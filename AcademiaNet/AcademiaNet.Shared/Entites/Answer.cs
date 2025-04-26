using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class Answer
{
    [Key]
    public int AnswerID { get; set; } // Identificador único para la respuesta

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = string.Empty; // Texto de la respuesta

    [Required]
    public bool IsCorrect { get; set; } // Indica si la respuesta es correcta

    [Required]
    public int QuestionID { get; set; }

    public Question? Question { get; set; }
}