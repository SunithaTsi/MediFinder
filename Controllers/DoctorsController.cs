using MediFinder.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediFinder.Controllers;

public class DoctorsController(IDoctorService doctorService) : Controller
{
    public async Task<IActionResult> Details(string slug)
    {
        var doctor = await doctorService.GetProfileAsync(slug);

        if (doctor is null)
        {
            return NotFound();
        }

        return View(doctor);
    }
}
