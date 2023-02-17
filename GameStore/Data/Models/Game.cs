using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models;

public class Game
{
    [Key] public int Id { get; set; }

    [Required(ErrorMessage = "You must give a name to the Game")]
    public string Name { get; set; } = default!;

    [Required] [Range(1, double.MaxValue)] public double Price { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
}