using GameStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly IAdminRepository _repo;

    public AdminController(IAdminRepository repo)
    {
        _repo = repo;
    }

    public ViewResult Index()
    {
        return View();
    }

    public async Task< ViewResult> Users(int pageNumber = 1)
    {
        var viewModel = await _repo.GetUsers(pageNumber);
        return View(viewModel);
    }

    public IActionResult Orders()
    {
        return View(nameof(Index));
    }
}