using GameStore.Data.ViewModels;
using GameStore.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.Services;

public class GamesService
{
    private readonly IGamesRepository _repo;

    public GamesService(IGamesRepository repo)
    {
        _repo = repo;
    }

    public async Task<GamesViewModel> GetGames(int pageNumber)
    {
        var genres = (await _repo.GetGenres()).Select(g => new SelectListItem(g.Name, g.Name)).ToList();

        if (pageNumber <= 0)
            return new GamesViewModel {GenresSelectList = genres};

        var games = await _repo.GetGames(pageNumber);
        return new GamesViewModel {Games = games.ToList(), GenresSelectList = genres};
    }

    public async Task<GamesViewModel> GetGames(string genre, int pageNumber)
    {
        var model = new GamesViewModel
        {
            GenresSelectList = (await _repo.GetGenres())
                .Select(g => new SelectListItem(g.Name, g.Name, g.Name == genre))
                .ToList()
        };

        if (string.IsNullOrWhiteSpace(genre) || pageNumber <= 0)
            return model;

        model.Games = await _repo.GetGamesByGenre(genre, pageNumber);
        model.GameGenre = genre;
        
        return model;
    }
}