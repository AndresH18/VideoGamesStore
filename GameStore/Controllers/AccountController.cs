using GameStore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult Login()
    {
        return View();
    }
}