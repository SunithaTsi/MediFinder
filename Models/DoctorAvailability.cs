using System.ComponentModel.DataAnnotations;

namespace MediFinder.Models;

public class DoctorAvailability
{
    public int DoctorAvailabilityId { get; set; }

    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public int ClinicId { get; set; }

    public Clinic Clinic { get; set; } = null!;

    [Required, StringLength(20)]
    public string DayOfWeek { get; set; } = string.Empty;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
