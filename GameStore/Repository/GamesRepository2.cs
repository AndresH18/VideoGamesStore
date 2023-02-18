using GameStore.Data;

namespace GameStore.Repository;

public class GamesRepository2
{
    private readonly GamesDbContext _db;

    public GamesRepository2(GamesDbContext db)
    {
        _db = db;
    }
    
    
}