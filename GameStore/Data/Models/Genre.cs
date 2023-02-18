using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameStore.Data.Models;

public class Genre
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; } = default!;

    [JsonIgnore] public virtual List<Game> Games { get; set; } = new();

    public override string ToString()
    {
        return Name;
    }
}