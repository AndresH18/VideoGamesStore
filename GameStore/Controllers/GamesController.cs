using GameStore.Data.Models;
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

    public async Task<IActionResult> Index(string? genre, int gamePage = 1)
    {
        IEnumerable<Game> data;
        
        if (genre is null)
            data = await _repo.GetGames(gamePage);
        else
            data = await _repo.GetGamesByGenre(genre, gamePage);
        
        return View(data.ToList());
    }
}