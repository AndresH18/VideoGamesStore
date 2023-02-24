using Microsoft.AspNetCore.Mvc;

namespace GameStore.Components;

public class NavigationMenuViewComponent : ViewComponent
{
    private static readonly (string, string)[] NavLinks = new[] {("", "")};

    public IViewComponentResult Invoke()
    {
        return View();
    }
}