namespace MediFinder.ViewModels;

public class DoctorListItemViewModel
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

    public decimal StartingFee { get; set; }

    public string PrimaryClinic { get; set; } = string.Empty;

    public string PrimaryArea { get; set; } = string.Empty;

    public IReadOnlyList<string> Specializations { get; set; } = [];
}
