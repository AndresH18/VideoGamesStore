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

    public async Task<IActionResult> Index(string? gameGenre, int gamePage = 1)
    {
        _logger.LogDebug("Called Index: Parameters={{gameGenre={genre}, gamePage={page}}}", gameGenre, gamePage);
        GamesViewModel model;
        if (string.IsNullOrWhiteSpace(gameGenre))
            model = await _service.GetGames(gamePage);
        else
            model = await _service.GetGames(gameGenre, gamePage);

        return View(model);
    }
}