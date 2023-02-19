using GameStore.Data.Models;
using GameStore.Repository;

namespace GameStore.Services;

public class CartService
{
    private readonly IGamesRepository _repo;

    public CartService(IGamesRepository repo)
    {
        _repo = repo;
    }

    public Task<Game?> GetGame(int gameId)
    {
        return _repo.GetGame(gameId);
    }
    
}