namespace MediFinder.ViewModels;

public class SearchResultsViewModel
{
    public SearchOptionsViewModel Search { get; set; } = new();

    public IReadOnlyList<SelectOptionViewModel> Areas { get; set; } = [];

    public IReadOnlyList<SelectOptionViewModel> Cities { get; set; } = [];

    public IReadOnlyList<SelectOptionViewModel> Specializations { get; set; } = [];

    public IReadOnlyList<DoctorListItemViewModel> Doctors { get; set; } = [];

    public int TotalDoctors { get; set; }
}
