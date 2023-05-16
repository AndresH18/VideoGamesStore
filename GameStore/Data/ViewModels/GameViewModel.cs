using System.ComponentModel.DataAnnotations;
using GameStore.Data.Models;
using GameStore.Controllers;

namespace GameStore.Data.ViewModels;

public class GameViewModel
{
    public GameViewModel()
    {
        
    }

    public GameViewModel(Game game)
    {
        Id = game.Id;
        Name = game.Name;
        Price = game.Price;
        Genre = game.Genre.Name;
    }
    public int Id { get; set; }

    [Required(ErrorMessage = "You must give a name to the Game")]
    public string Name { get; set; } = default!;

    [Required] [Range(1, double.MaxValue)] public double Price { get; set; }

    [Required] public string Genre { get; set; } = default!;

    public string? ReturnUrl { get; set; } = nameof(AdminController.Products);
}