using GameStore.Data.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

public class CartController : Controller
{
    private readonly CartService _service;

    private readonly Cart _cart;

    public CartController(CartService service, Cart cart)
    {
        _service = service;
        _cart = cart;
    }

    public IActionResult Index()
    {
        return View(_cart);
    }

    public async Task<IActionResult> AddGame([FromForm(Name = "Id")] int gameId)
    {
        if (gameId <= 0)
            return RedirectToAction(nameof(Index));
        
        var game = await _service.GetGame(gameId);
        if (game != null)
        {
            _cart.AddItem(game, 1);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveGame([FromForm(Name = "cartItem.Game.Id")] int gameId)
    {
        if (gameId <= 0) 
            return RedirectToAction(nameof(Index));
        
        var game = await _service.GetGame(gameId);
        if (game != null)
        {
            _cart.RemoveItem(game);
        }

        return RedirectToAction(nameof(Index));
    }
}