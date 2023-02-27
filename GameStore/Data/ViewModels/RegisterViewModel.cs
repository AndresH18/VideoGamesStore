using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.ViewModels;

public class RegisterViewModel : LoginViewModel
{
    [Required] public string UserName { get; set; } = default!;
}