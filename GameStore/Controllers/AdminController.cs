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

    public IActionResult Orders()
    {
        return View(nameof(Index));
    }

    public IActionResult Products()
    {
        return View(nameof(Index));
    }

    private void AddModelErrors(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
    }
}