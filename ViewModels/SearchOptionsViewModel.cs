namespace MediFinder.ViewModels;

public class SearchOptionsViewModel
{
    public string? Area { get; set; }

    public string? City { get; set; }

    public string? Specialization { get; set; }

    public string? Keyword { get; set; }

    public string SortBy { get; set; } = "rating";

    public int Page { get; set; } = 1;
}
