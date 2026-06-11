namespace MediFinder.ViewModels;

public class HomeViewModel
{
    public SearchOptionsViewModel Search { get; set; } = new();

    public IReadOnlyList<SelectOptionViewModel> Areas { get; set; } = [];

    public IReadOnlyList<SelectOptionViewModel> Cities { get; set; } = [];

    public IReadOnlyList<SelectOptionViewModel> Specializations { get; set; } = [];

    public IReadOnlyList<DoctorListItemViewModel> FeaturedDoctors { get; set; } = [];
}
