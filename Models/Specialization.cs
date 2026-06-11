using System.ComponentModel.DataAnnotations;

namespace MediFinder.Models;

public class Specialization
{
    public int SpecializationId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(400)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
}
