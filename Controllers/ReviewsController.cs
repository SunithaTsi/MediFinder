using MediFinder.Services;
using MediFinder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediFinder.Controllers;

public class ReviewsController(IReviewService reviewService, IDoctorService doctorService) : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string doctorSlug, ReviewFormViewModel review)
    {
        if (!ModelState.IsValid)
        {
            var profile = await doctorService.GetProfileAsync(doctorSlug);
            if (profile is null)
            {
                return NotFound();
            }

            profile.ReviewForm = review;
            return View("~/Views/Doctors/Details.cshtml", profile);
        }

        var saved = await reviewService.AddReviewAsync(review);
        if (!saved)
        {
            return NotFound();
        }

        TempData["ReviewMessage"] = "Thanks. Your review has been submitted for moderation.";
        return RedirectToAction("Details", "Doctors", new { slug = doctorSlug });
    }
}
