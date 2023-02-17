using GameStore.Data.Models;

namespace GameStore.Repository;

public interface IGamesRepository
{
    public Task<IEnumerable<Game>> Get(int page);
}