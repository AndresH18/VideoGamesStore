using GameStore.Data;
using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository;

public class GamesRepository : IGamesRepository
{
    private const int PageSize = 10;
    private readonly IGamesDb _db;

    public GamesRepository(IGamesDb db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Game>> Get(string? category, int page)
    {
        return await _db.Games
            .Include(g => g.Category)
            .Where(g => category == null || g.Category.Name == category)
            .Skip(PageSize * (page - 1))
            .Take(PageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetCategories()
    {
        return await _db.Categories.Select(c => c.Name).Distinct().ToListAsync();
    }
}