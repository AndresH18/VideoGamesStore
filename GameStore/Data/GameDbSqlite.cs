using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class GameDbSqlite : DbContext, IGamesDb
{
    public DbSet<Game> Games { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=GamesDb");
        }
    }
}