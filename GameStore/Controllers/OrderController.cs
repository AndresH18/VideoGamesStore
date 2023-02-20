using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

public class OrderController : Controller
{
    private readonly OrderService _service;

    private readonly Cart _cart;

    public OrderController(OrderService service, Cart cart)
    {
        _service = service;
        _cart = cart;
    }

    public IActionResult Cart(string? returnUrl)
    {
        return View(new CartViewModel
        {
            Cart = _cart,
            ReturnUrl = returnUrl
        });
    }

    public async Task<IActionResult> AddGame([FromForm(Name = "Id")] int gameId, [FromForm] string? returnUrl)
    {
        if (gameId <= 0)
            return RedirectToAction(nameof(Cart));

        var game = await _service.GetGame(gameId);
        if (game != null)
        {
            _cart.AddItem(game, 1);
        }

        return RedirectToAction(nameof(Cart), new {returnUrl});
    }

    public async Task<IActionResult> RemoveGame([FromForm(Name = "cartItem.Game.Id")] int gameId,
        [FromForm] string? returnUrl)
    {
        if (gameId <= 0)
            return RedirectToAction(nameof(Cart));

        var game = await _service.GetGame(gameId);
        if (game != null)
        {
            _cart.RemoveItem(game);
        }

        return RedirectToAction(nameof(Cart), new {returnUrl});
    }

    public IActionResult Checkout()
    {
        return View(new Order());
    }


    [HttpPost]
    public async Task<IActionResult> Checkout(Order order)
    {
        // if (!_cart.CartItems.Any())
        //     ModelState.AddModelError("", "Sorry, your cart is Empty!");
        //
        // if (!ModelState.IsValid)
        //     return View();

        // if (!ModelState.IsValid || !_cart.CartItems.Any())
        if (!(ModelState.IsValid && _cart.CartItems.Any()))
        {
            if (!_cart.CartItems.Any())
                ModelState.AddModelError("", "Sorry, your cart is Empty!");

            return View();
        }

        // order.Items = _cart.CartItems;
        // TODO: view if method should return null
        order = await _service.SaveOrder(order, _cart);
        _cart.Clear();
        return RedirectToAction(nameof(Completed), new {orderId = order.Id});
    }

    public IActionResult Completed(int orderId)
    {
        return View(orderId);
    }
}