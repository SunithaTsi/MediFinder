using System.ComponentModel.DataAnnotations;

namespace MediFinder.Models;

public class Clinic
{
    public int ClinicId { get; set; }

    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public int AreaId { get; set; }

    public Area Area { get; set; } = null!;

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(300)]
    public string Address { get; set; } = string.Empty;

    [Required, StringLength(25)]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress, StringLength(160)]
    public string? Email { get; set; }

    [Range(0, 100000)]
    public decimal ConsultationFee { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public bool IsActive { get; set; } = true;
}
