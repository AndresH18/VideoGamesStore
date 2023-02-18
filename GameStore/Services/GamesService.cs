using GameStore.Data.ViewModels;
using GameStore.Repository;

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
        var genres = await _repo.GetGenres();

        if (pageNumber <= 0)
            return new GamesViewModel {Genres = genres.ToList()};

        var games = await _repo.GetGames(pageNumber);

        return new GamesViewModel {Games = games.ToList(), Genres = genres.ToList()};
    }
}