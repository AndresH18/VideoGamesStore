using GameStore.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.Data.ViewModels;

public class GamesViewModel
{
    public IEnumerable<Game> Games { get; set; } = Enumerable.Empty<Game>();
    public IEnumerable<SelectListItem> GenresSelectList { get; set; } = Enumerable.Empty<SelectListItem>();

    public string? GameGenre { get; set; }
}