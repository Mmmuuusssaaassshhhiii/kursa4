using Microsoft.AspNetCore.Mvc;

namespace kursa4.Controllers;

public class InfoController : Controller
{
    public IActionResult AboutApp()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult AboutDeveloper()
    {
        return View();
    }
}