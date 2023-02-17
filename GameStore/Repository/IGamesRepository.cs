using GameStore.Data.Models;

namespace GameStore.Repository;

public interface IGamesRepository
{
    public Task<IEnumerable<Game>> Get(string? category, int page);
    public Task<IEnumerable<string>> GetCategories();
}