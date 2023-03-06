using System.Security.Claims;
using GameStore.Data;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GameStore.Services;

public class AccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly Cart _cart;


    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        Cart cart)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _cart = cart;
    }

    public async Task<SignInResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            await Logout();
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }

        return SignInResult.Failed;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
        _cart.Clear();
    }

    public async Task<IdentityResult> Register(RegisterViewModel registerModel)
    {
        var user = new ApplicationUser
        {
            UserName = registerModel.UserName, Email = registerModel.Email
        };
        var result = await _userManager.CreateAsync(user, registerModel.Password);
        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, "user");

        return result;
    }

    public bool IsSignedIn(ClaimsPrincipal user)
    {
        return _signInManager.IsSignedIn(user);
    }
}