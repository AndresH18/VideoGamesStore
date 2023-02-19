using GameStore.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Components;

public class CartSummaryViewComponent : ViewComponent
{
    private readonly Cart _cart;

    public CartSummaryViewComponent(Cart cart)
    {
        _cart = cart;
    }

    public IViewComponentResult Invoke()
    {
        return View(_cart);
    }
}