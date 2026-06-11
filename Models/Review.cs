using System.ComponentModel.DataAnnotations;

namespace MediFinder.Models;

public class Review
{
    public int ReviewId { get; set; }

    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    [Required, StringLength(80)]
    public string ReviewerName { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required, StringLength(800)]
    public string Comment { get; set; } = string.Empty;

    public bool IsApproved { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
