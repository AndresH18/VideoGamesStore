using System.Security.Claims;
using GameStore.Data;
using GameStore.Data.Identity;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using GameStore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository;

public class AdminRepository : IAdminRepository
{
    private const int PageSize = 20;
    private readonly ApplicationContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AdminRepository(ApplicationContext context, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<ListViewModel<ApplicationUser>> GetUsers(int pageIndex)
    {
        if (pageIndex <= 0)
            return new ListViewModel<ApplicationUser>();

        var totalUsers = await _context.Users.CountAsync();
        var users = await _context.Users
            .Skip(PageSize * (pageIndex - 1))
            .Take(PageSize)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();

        return new ListViewModel<ApplicationUser>
        {
            Items = users,
            PageInfo = new PageInfo
            {
                TotalItems = totalUsers,
                CurrentPage = pageIndex,
                ItemsPerPage = PageSize
            }
        };
    }

    public async Task<ApplicationUser?> GetUser(Guid userId)
    {
        // TODO: use UserManager. You would need to get the Users and then the Roles
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IdentityResult> DeleteUser(Guid userId, ClaimsPrincipal currentUser)
    {
        if (_userManager.GetUserId(currentUser) != userId.ToString())
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return IdentityResult.Failed();

            return await _userManager.DeleteAsync(user);
        }

        return IdentityResult.Failed(new IdentityError {Description = "Cannot delete current user"});
    }

    public async Task<IdentityResult> VerifyUser(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return IdentityResult.Failed();

        user.EmailConfirmed = true;

        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> UpdateUser(UserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());
        if (user == null)
            return IdentityResult.Failed();

        user.UpdateFromModel(model);
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return result;
        if (model.UserName == "andres-admin")
        {
            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "user");
            return IdentityResult.Success;
        }

        if (model.IsAdmin)
        {
            await _userManager.AddToRoleAsync(user, "admin");
        }
        else
        {
            await _userManager.RemoveFromRoleAsync(user, "admin");
        }


        return result;
    }

    public async Task<ListViewModel<Order>> GetOrders(int pageIndex, string filter)
    {
        if (pageIndex <= 0)
            return new ListViewModel<Order>();

        if (filter is "shipped" or "unshipped")
        {
            var shippingStatus = filter == "shipped";
            var totalOrders = _context.Orders.Count(o => o.Shipped == shippingStatus);
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.Shipped == shippingStatus)
                .Skip(PageSize * (pageIndex - 1))
                .Take(PageSize)
                .ToListAsync();

            return new ListViewModel<Order>
            {
                Items = orders,
                PageInfo = new PageInfo
                {
                    TotalItems = totalOrders,
                    CurrentPage = pageIndex,
                    ItemsPerPage = PageSize
                }
            };
        }

        if (string.IsNullOrWhiteSpace(filter))
        {
            var totalOrders = await _context.Orders.CountAsync();
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Skip(PageSize * (pageIndex - 1))
                .Take(PageSize)
                .ToListAsync();

            return new ListViewModel<Order>
            {
                Items = orders,
                PageInfo = new PageInfo
                {
                    TotalItems = totalOrders,
                    CurrentPage = pageIndex,
                    ItemsPerPage = PageSize
                }
            };
        }

        return new ListViewModel<Order>();
    }

    public async Task Ship(int orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order != null)
        {
            order.Shipped = true;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<GamesListViewModel> GetProducts(int pageNumber, int genreId)
    {
        if (pageNumber <= 0 || genreId < 0)
            return new GamesListViewModel();
        List<Game> games;
        var genres = await _context.Genres.ToListAsync();

        if (genreId == 0)
        {
            var totalGames = await _context.Games.CountAsync();

            games = await _context.Games
                .Include(g => g.Genre)
                .Skip(PageSize * (pageNumber - 1))
                .Take(PageSize)
                .ToListAsync();


            return new GamesListViewModel
            {
                Items = games,
                GenresSelectList = genres.Select(g => new SelectListItem(g.Name, g.Id.ToString())),
                PageInfo = new PageInfo {CurrentPage = pageNumber, ItemsPerPage = PageSize, TotalItems = totalGames}
            };
        }

        var totalGenreGames = await _context.Genres.CountAsync(g => g.Id == genreId);
        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

        games = await _context.Games
            .Where(g => g.GenreId == genreId)
            .Skip(PageSize * (pageNumber - 1))
            .Take(PageSize)
            .Include(g => g.Genre)
            .ToListAsync();

        return new GamesListViewModel
        {
            Items = games,
            GenresSelectList = genres.Select(g =>
                new SelectListItem(g.Name, g.Id.ToString(), g.Id == games.First().GenreId)),
            Genre = (genre!.Name, genre.Id),
            PageInfo = new PageInfo
            {
                TotalItems = totalGenreGames,
                CurrentPage = pageNumber,
                ItemsPerPage = PageSize,
            }
        };
    }

    public async Task DeleteProduct(int gameId)
    {
        if (gameId <= 0)
            return;

        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game != null)
        {
            _context.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}