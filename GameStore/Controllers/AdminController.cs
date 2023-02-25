using GameStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

public class AdminController : Controller
{

    private readonly AdminRepository _repo;

    public AdminController(AdminRepository repo)
    {
        _repo = repo;
    }

    // GET
    public IActionResult Index(string? activeNav)
    {
        
        return View();
    }
}