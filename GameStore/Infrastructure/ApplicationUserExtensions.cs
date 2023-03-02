using GameStore.Data.Identity;
using GameStore.Data.ViewModels;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;

namespace GameStore.Infrastructure;

public static class ApplicationUserExtensions
{
    public static void UpdateFromModel(this ApplicationUser user, UserViewModel model)
    {
        user.UserName = model.UserName;
        user.Email = model.Email;
    }
}