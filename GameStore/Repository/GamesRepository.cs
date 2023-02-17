using GameStore.Data.Models;

namespace GameStore.Repository;

public class GamesRepository : IGamesRepository
{
    public Task<IEnumerable<Game>> Get(int page)
    {
        return Task.FromResult(Enumerable.Range(page, 10).Select(i => new Game {Id = i, Name = i.ToString()}));
    }
}