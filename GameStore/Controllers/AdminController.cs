using GameStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

public class AdminController : Controller
{
    private readonly IAdminRepository _repo;

    public AdminController(IAdminRepository repo)
    {
        _repo = repo;
    }

    // GET
    public IActionResult Index(string? activeNav)
    {
        return View();
    }

    public IActionResult Users()
    {
        return View(nameof(Index));
    }

    public IActionResult Orders()
    {
        return View(nameof(Index));
    }
}