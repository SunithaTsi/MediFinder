using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MediFinder.Models;
using MediFinder.Services;

namespace MediFinder.Controllers;

public class HomeController(IDoctorService doctorService) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await doctorService.GetHomeAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
