using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.ViewModels;

public class UserViewModel
{
    public UserViewModel()
    {
    }

    public UserViewModel(ApplicationUser user)
    {
        UserId = user.Id;
        UserName = user.UserName!;
        Email = user.Email!;
        // Roles = user.UserRoles.Select(ur => ur.Role);
        IsAdmin = user.UserRoles.Select(ur => ur.Role).Any(r => r.NormalizedName == "ADMIN");
    }

    public Guid UserId { get; set; }
    [Required] public string UserName { get; set; } = default!;
    [Required, EmailAddress] public string Email { get; set; } = default!;
    public string? Password { get; set; }

    // [BindNever] public IEnumerable<ApplicationRole> Roles { get; set; }

    public bool IsAdmin { get; set; }

    // public string? ReturnUrl { get; set; }
}