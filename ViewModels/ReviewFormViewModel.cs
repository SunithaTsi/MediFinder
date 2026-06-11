using System.ComponentModel.DataAnnotations;

namespace MediFinder.ViewModels;

public class ReviewFormViewModel
{
    public int DoctorId { get; set; }

    [Required, StringLength(80)]
    public string ReviewerName { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    [Required, StringLength(800)]
    public string Comment { get; set; } = string.Empty;
}
