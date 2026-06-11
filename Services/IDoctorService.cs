using MediFinder.ViewModels;

namespace MediFinder.Services;

public interface IDoctorService
{
    Task<HomeViewModel> GetHomeAsync();

    Task<SearchResultsViewModel> SearchAsync(SearchOptionsViewModel search);

    Task<DoctorProfileViewModel?> GetProfileAsync(string slug);
}
