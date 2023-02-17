using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public interface IGamesDb
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
}