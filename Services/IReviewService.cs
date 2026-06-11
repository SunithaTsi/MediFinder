using MediFinder.ViewModels;

namespace MediFinder.Services;

public interface IReviewService
{
    Task<bool> AddReviewAsync(ReviewFormViewModel review);
}
