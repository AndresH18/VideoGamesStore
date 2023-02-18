using GameStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Components;

public class NavigationMenuViewComponent : ViewComponent
{
    private readonly IGamesRepository _repo;

    public NavigationMenuViewComponent(IGamesRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Invokes the <b>View Component</b>.
    /// </summary>
    /// <returns>The View Component.</returns>
    /// <param name="paramName">The query string <b>key</b> that determines if the navigation item is active.</param>
    public async Task<IViewComponentResult> InvokeAsync(string paramName)
    {
    //     ViewBag.Selected = RouteData.Values[paramName]!;
    //     var list = await _repo.GetGenres();
        return View(null);
    }
}