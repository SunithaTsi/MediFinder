using MediFinder.Services;
using MediFinder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediFinder.Controllers;

public class SearchController(IDoctorService doctorService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] SearchOptionsViewModel search)
    {
        return View(await doctorService.SearchAsync(search));
    }
}
