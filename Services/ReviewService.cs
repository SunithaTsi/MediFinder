using MediFinder.Data;
using MediFinder.Models;
using MediFinder.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MediFinder.Services;

public class ReviewService(ApplicationDbContext db) : IReviewService
{
    public async Task<bool> AddReviewAsync(ReviewFormViewModel review)
    {
        var doctor = await db.Doctors.FirstOrDefaultAsync(item => item.DoctorId == review.DoctorId && item.IsActive);
        if (doctor is null)
        {
            return false;
        }

        db.Reviews.Add(new Review
        {
            DoctorId = review.DoctorId,
            ReviewerName = review.ReviewerName.Trim(),
            Rating = review.Rating,
            Comment = review.Comment.Trim(),
            IsApproved = false,
            CreatedAt = DateTime.UtcNow
        });

        await db.SaveChangesAsync();
        return true;
    }
}
