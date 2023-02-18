using GameStore.Data.Models;

namespace GameStore.Repository;

public interface IGamesRepository
{
    // public Task<IEnumerable<Game>> Get(string? category, int page);

    public Task<Game?> GetGame(int id);
    public Task<IEnumerable<Game>> GetGames(int pageNumber);
    public Task<IEnumerable<Game>> GetGamesByGenre(int genreId, int pageSize);
    public Task<IEnumerable<Game>> GetGamesByGenre(string genre, int pageSize);
    public Task<IEnumerable<string>> GetCategories();
}