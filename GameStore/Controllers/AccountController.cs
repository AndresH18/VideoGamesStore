using GameStore.Data;
using GameStore.Data.ViewModels;
using GameStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameStore.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Login(string returnUrl = "/")
    {
        if (!_accountService.IsSignedIn(User))
            return View(new LoginViewModel {ReturnUrl = returnUrl});

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if ((await _accountService.Login(model.Email, model.Password)).Succeeded)
        {
            return Redirect(model.ReturnUrl ?? "/");
        }

        ModelState.AddModelError("", "Invalid username or password");

        return View(model);
    }

    public ViewResult Register(string returnUrl = "/")
    {
        return View(new RegisterViewModel {ReturnUrl = returnUrl});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _accountService.Register(model);

            if (result.Succeeded)
                return RedirectToAction(nameof(Login));

            AddModelErrors(result.Errors);
        }
        else
        {
            ModelState.AddModelError("", "Invalid Username or Password");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _accountService.Logout();

        return RedirectToAction(nameof(Login));
    }

    private void AddModelErrors(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
    }
}