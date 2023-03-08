using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using GameStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[Authorize(Roles = "admin, root")]
public class AdminController : Controller
{
    private readonly IAdminRepository _repo;

    public AdminController(IAdminRepository repo)
    {
        _repo = repo;
    }

    public ViewResult Index()
    {
        return View();
    }

    public async Task<ViewResult> Users(int pageNumber = 1)
    {
        var viewModel = await _repo.GetUsers(pageNumber);
        return View(viewModel);
    }

    public async Task<ViewResult> EditUser(Guid userId)
    {
        var user = await _repo.GetUser(userId);

        return user != null
            ? View(new UserViewModel(user))
            : View(nameof(Users));
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UserViewModel model)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(model.UserId.ToString()))
            return View(model);


        var result = await _repo.UpdateUser(model);
        if (!result.Succeeded)
        {
            AddModelErrors(result.Errors);
            return View(model);
        }

        return RedirectToAction(nameof(Users));
    }

    public async Task<RedirectResult> DeleteUser(Guid userId, string returnUrl = nameof(Users))
    {
        await _repo.DeleteUser(userId, User);
        return Redirect(returnUrl);
    }

    public async Task<IActionResult> Orders(int pageNumber = 1, string select = "")
    {
        ListViewModel<Order> model;
        if (select is "shipped" or "unshipped" or "")
        {
            ViewBag.filter = select;
            model = await _repo.GetOrders(pageNumber, select);
        }
        else
        {
            model = await _repo.GetOrders(pageNumber, "");
        }

        return View(model);
    }

    public async Task<IActionResult> Products(int pageNumber = 1, int genreId = 0)
    {
        var model = await _repo.GetProducts(pageNumber, genreId);

        return View(model);
    }

    public RedirectResult DeleteProduct(int gameId, string returnUrl = nameof(Products))
    {
        if (gameId != 0)
        {
            _repo.DeleteProduct(gameId);
        }

        return Redirect(returnUrl);
    }

    public async Task<IActionResult> EditProduct(int gameId, string returnUrl = nameof(Products))
    {
        if (gameId == 0)
        {
            // create new game
            return View(new GameViewModel { ReturnUrl = returnUrl });
        }

        // edit Game
        var game = await _repo.GetProduct(gameId);
        if (game != null)
            return View(new GameViewModel(game) { ReturnUrl = returnUrl });

        return Redirect(returnUrl);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct([FromForm] GameViewModel model)
    {
        if (!ModelState.IsValid)
        {
            
            return View(model);
        }

        if (model.Id != 0)
            await _repo.UpdateProduct(model);
        else 
            await _repo.CreateProduct(model);

        return Redirect(nameof(Products));

    }

    [HttpPost]
    public async Task<RedirectResult> ShipOrder([FromForm] int orderId, [FromForm] string returnUrl = nameof(Orders))
    {
        await _repo.Ship(orderId);
        return Redirect(returnUrl);
    }

    private void AddModelErrors(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
    }
}