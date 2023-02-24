using GameStore.Data.Identity;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Services;

public class AccountService
{
    private readonly UserManager<ApplicationUser> _usermanager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signInManager)
    {
        _usermanager = usermanager;
        _signInManager = signInManager;
    }

    public async Task<bool> Login(LoginViewModel model)
    {
        var user = await _usermanager.FindByEmailAsync(model.UserName);
        if (user != null)
        {
            await Logout();
        }
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
        // TODO: clear Cart
    }
}