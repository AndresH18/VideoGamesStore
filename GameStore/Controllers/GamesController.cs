using GameStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

public class GamesController : Controller
{
    private readonly ILogger<GamesController> _logger;
    private readonly IGamesRepository _repo;

    public GamesController(ILogger<GamesController> logger, IGamesRepository repository)
    {
        _logger = logger;
        _repo = repository;
    }

    public async Task<IActionResult> Index(int gamePage, string? category)
    {
        var data = await _repo.Get(category, gamePage);
        return View(data.ToList());
    }
}