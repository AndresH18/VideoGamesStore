using GameStore.Data;
using GameStore.Data.Identity;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository;

public class AdminRepository : IAdminRepository
{
    private const int PageSize = 20;
    private readonly UsersContext _usersContext;
    private readonly GamesDbContext _gamesContext;

    public AdminRepository(UsersContext usersContext, GamesDbContext gamesContext)
    {
        _usersContext = usersContext;
        _gamesContext = gamesContext;
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
                TotalItems = (uint) totalGames,
                CurrentPage = (uint) pageIndex,
                ItemsPerPage = PageSize,
            }
        };
    }
}