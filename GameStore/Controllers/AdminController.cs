using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

public class AdminController : Controller
{


    // GET
    public IActionResult Index(string? activeNav)
    {
        
        return View();
    }
}