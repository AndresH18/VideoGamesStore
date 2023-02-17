using GameStore.Data.Models;

namespace GameStore.Repository;

public class GamesRepository : IGamesRepository
{
    public Task<IEnumerable<Game>> Get(string? category, int page)
    {
        return Task.FromResult(Enumerable.Range(page, 10).Select(i => new Game
            {Id = i, Name = i.ToString(), Category = Guid.NewGuid().ToString()}));
    }

    public Task<IEnumerable<string>> GetCategories()
    {
        return Task.FromResult(Enumerable.Range(0, 10).Select(i => i.ToString()));
    }
}