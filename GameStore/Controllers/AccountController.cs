using GameStore.Data;
using GameStore.Data.Identity;
using GameStore.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameStore.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<GameStoreUser> _userManager;
    private readonly SignInManager<GameStoreUser> _signInManager;

    public AccountController(UserManager<GameStoreUser> userManager, SignInManager<GameStoreUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public ViewResult Login(string returnUrl = "/")
    {
        return View(new LoginViewModel {ReturnUrl = returnUrl});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                {
                    return Redirect(model.ReturnUrl ?? "/");
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
        }

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
            var user = new GameStoreUser {UserName = model.UserName, Email = model.UserName, EmailConfirmed = true};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(Login));
            else
                AddModelErrors(result.Errors);
        }
        else
        {
            ModelState.AddModelError("", "Invalid Username or Password");
        }

        return View(model);
    }


    private void AddModelErrors(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            if (error.Code is ("DuplicateUserName" or "DuplicateEmail"))
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}