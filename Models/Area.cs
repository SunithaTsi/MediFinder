using System.ComponentModel.DataAnnotations;

namespace MediFinder.Models;

public class Area
{
    public int AreaId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string City { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string State { get; set; } = string.Empty;

    [StringLength(12)]
    public string PinCode { get; set; } = string.Empty;

    [Required, StringLength(140)]
    public string Slug { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
}
