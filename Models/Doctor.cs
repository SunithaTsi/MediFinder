using System.ComponentModel.DataAnnotations;

namespace MediFinder.Models;

public class Doctor
{
    public int DoctorId { get; set; }

    [Required, StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(160)]
    public string Slug { get; set; } = string.Empty;

    [Required, StringLength(160)]
    public string Qualification { get; set; } = string.Empty;

    [Range(0, 70)]
    public int ExperienceYears { get; set; }

    [StringLength(1200)]
    public string Bio { get; set; } = string.Empty;

    [StringLength(300)]
    public string ProfileImageUrl { get; set; } = string.Empty;

    [Range(0, 5)]
    public decimal AverageRating { get; set; }

    public int ReviewCount { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();

    public ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<DoctorAvailability> Availabilities { get; set; } = new List<DoctorAvailability>();
}
