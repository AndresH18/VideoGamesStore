using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using GameStore.Repository;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

public class GamesController : Controller
{
    private readonly ILogger<GamesController> _logger;
    private readonly GamesService _service;

    public GamesController(ILogger<GamesController> logger, GamesService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IActionResult> Index(int gamePage = 1)
    {
        var model = await _service.GetGames(gamePage);
        
        return View(model);
    }

    public async Task<IActionResult> Genre(string genre, int gamePage)
    {
        if (string.IsNullOrWhiteSpace(genre))
            return RedirectToAction(nameof(Index));

        return null;

    }
}