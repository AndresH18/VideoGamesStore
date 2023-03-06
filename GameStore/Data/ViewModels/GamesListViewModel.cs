using GameStore.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.Data.ViewModels;

public class GamesListViewModel : ListViewModel<Game>
{
    public IEnumerable<SelectListItem> GenresSelectList { get; set; } = Enumerable.Empty<SelectListItem>();
    public (string genre, int id)? Genre { get; set; }
}