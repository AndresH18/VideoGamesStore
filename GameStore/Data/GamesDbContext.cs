﻿using Microsoft.EntityFrameworkCore;
using GameStore.Data.Models;

namespace GameStore.Data;

public class GamesDbContext : DbContext
{
    public GamesDbContext()
    {
    }

    public GamesDbContext(DbContextOptions<GamesDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Game> Games { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=localhost; Initial Catalog=GamesStore; Trusted_Connection=true; TrustServerCertificate=true");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre {Id = 1, Name = "Metroid-vania"},
            new Genre {Id = 2, Name = "Puzzle"});
    }
}