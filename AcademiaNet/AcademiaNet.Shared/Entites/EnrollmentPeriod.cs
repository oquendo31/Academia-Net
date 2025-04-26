using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Shared.Entites;

public class EnrollmentPeriod
{
    [Key]
    public int EnrollmentPeriodID { get; set; }

    [MaxLength(100)]
    [Required]
    public string PeriodName { get; set; } = null!;

    [Required]
    public DateTime StartDateEnrollment { get; set; }

    [Required]
    public DateTime EndDateEnrollment { get; set; }

    [Required]
    public DateTime StartDateExam { get; set; }

    [Required]
    public DateTime EndDateExam { get; set; }

    [Required]
    public int InstitutionID { get; set; }

    public Institution? Institution { get; set; }
    public ICollection<Exam>? Exams { get; set; }
    public int ExamsCount => Exams == null ? 0 : Exams.Count;
    public ICollection<Enrollment>? Enrollments { get; set; }
    public int EnrollmentsCount => Enrollments == null ? 0 : Enrollments.Count;

    public ICollection<Notification>? Notifications { get; set; }
    public int NotificationsCount => Notifications == null ? 0 : Notifications.Count;
}