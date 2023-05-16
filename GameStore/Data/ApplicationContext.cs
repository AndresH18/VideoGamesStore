using GameStore.Data.Identity;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>,
    ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(b =>
        {
            // Each User can have many UserClaims
            b.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            b.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            b.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            // b.HasMany(e => e.UserRoles)
            //     .WithOne()
            //     .HasForeignKey(ur => ur.UserId)
            //     .IsRequired();


            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        builder.Entity<ApplicationRole>(b =>
        {
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });


        builder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Metroid-vania" },
            new Genre { Id = 2, Name = "Puzzle" },
            new Genre { Id = 3, Name = "First Person Shooter" });
        builder.Entity<Game>().HasData(
            new Game { Id = 1, Name = "Metroid Prime Remastered", GenreId = 1, Price = 100_000 },
            new Game { Id = 2, Name = "Doom Eternal", GenreId = 3, Price = 150_000 });

        builder.Entity<Order>(o => { o.HasMany(order => order.OrderItems).WithOne(ot => ot.Order); });
    }
}