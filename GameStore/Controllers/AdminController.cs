using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using GameStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[Authorize(Roles = "admin")]
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

    // public ViewResult CreateUser()
    // {
    //     return View(nameof(EditUser), new UserViewModel());
    // }

    // public async Task<RedirectResult> VerifyUser(Guid userId, string returnUrl = "Users")
    // {
    //     await _repo.VerifyUser(userId);
    //     return Redirect(returnUrl);
    // }
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

    public IActionResult EditProduct(int gameId, string returnUrl = nameof(Products))
    {
        if (gameId == 0)
        {
            // create new game
            return View(nameof(Products));
        }

        // edit Game
        return View(nameof(Products));
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