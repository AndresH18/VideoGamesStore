using GameStore.Data;
using GameStore.Data.Identity;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using GameStore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository;

public class AdminRepository : IAdminRepository
{
    private const int PageSize = 20;
    private readonly UsersContext _usersContext;
    private readonly GamesDbContext _gamesContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminRepository(UsersContext usersContext, GamesDbContext gamesContext,
        UserManager<ApplicationUser> userManager)
    {
        _usersContext = usersContext;
        _gamesContext = gamesContext;
        _userManager = userManager;
    }

    public async Task<ListViewModel<Game>> GetProducts(int pageIndex)
    {
        if (pageIndex <= 0)
            return new ListViewModel<Game>();

        var totalGames = await _gamesContext.Games.CountAsync();
        var games = await _gamesContext.Games.Skip(PageSize * (pageIndex - 1)).Take(PageSize).ToListAsync();

        return new ListViewModel<Game>
        {
            Items = games,
            PageInfo = new PageInfo
            {
                TotalItems = totalGames,
                CurrentPage = pageIndex,
                ItemsPerPage = PageSize,
            }
        };
    }

    public async Task<ListViewModel<ApplicationUser>> GetUsers(int pageIndex)
    {
        if (pageIndex <= 0)
            return new ListViewModel<ApplicationUser>();

        var totalUsers = await _usersContext.Users.CountAsync();
        var users = await _usersContext.Users
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
        return await _usersContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);
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

        // result = await _userManager.ChangePasswordAsync(user, "old password", "new password");
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
}