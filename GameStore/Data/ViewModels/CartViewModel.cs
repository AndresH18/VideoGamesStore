using GameStore.Data.Models;

namespace GameStore.Data.ViewModels;

public class CartViewModel
{
    public required Cart Cart { get; set; } 
    public string? ReturnUrl { get; set; }
}