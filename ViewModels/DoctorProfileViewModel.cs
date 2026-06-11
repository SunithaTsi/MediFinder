namespace MediFinder.ViewModels;

public class DoctorProfileViewModel
{
    public int DoctorId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Qualification { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public string Bio { get; set; } = string.Empty;

    public string ProfileImageUrl { get; set; } = string.Empty;

    public decimal AverageRating { get; set; }

    public int ReviewCount { get; set; }

    public IReadOnlyList<string> Specializations { get; set; } = [];

    public IReadOnlyList<ClinicViewModel> Clinics { get; set; } = [];

    public IReadOnlyList<string> Availability { get; set; } = [];

    public IReadOnlyList<ReviewListItemViewModel> Reviews { get; set; } = [];

    public ReviewFormViewModel ReviewForm { get; set; } = new();
}
