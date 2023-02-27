using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.ViewModels;

public class LoginViewModel
{
    [Required, EmailAddress] public string Email { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;
    public string? ReturnUrl { get; set; }
}