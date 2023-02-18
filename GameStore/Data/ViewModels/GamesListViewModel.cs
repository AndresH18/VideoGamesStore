using GameStore.Data.Models;

namespace GameStore.Data.ViewModels;

public class GamesListViewModel
{
    public IEnumerable<Game> Games { get; init; } = Enumerable.Empty<Game>();
    public IEnumerable<Genre> Genres { get; init; } = Enumerable.Empty<Genre>();
}