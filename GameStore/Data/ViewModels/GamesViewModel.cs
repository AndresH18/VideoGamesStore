using GameStore.Data.Models;

namespace GameStore.Data.ViewModels;

public class GamesViewModel
{
    public IEnumerable<Game> Games { get; set; } = Enumerable.Empty<Game>();
    public IEnumerable<Genre> Genres { get; set; } = Enumerable.Empty<Genre>();
}