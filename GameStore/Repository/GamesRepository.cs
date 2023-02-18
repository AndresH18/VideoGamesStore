using GameStore.Data;
using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository;

public class GamesRepository : IGamesRepository
{
    private const int PageSize = 10;
    private readonly GamesDbContext _db;

    public GamesRepository(GamesDbContext db)
    {
        _db = db;
    }

    // public async Task<GamesListViewModel> Get(string? genre, int page)
    // {
    //     if (page < 0)
    //         return new GamesListViewModel {Games = Enumerable.Empty<Game>(), Genres = Enumerable.Empty<Genre>()};
    //
    //     var games = await _db.Games.Include(g => g.Genre)
    //         .Where(g => genre == null || g.Genre.Name == genre)
    //         .Skip(PageSize * (page - 1))
    //         .Take(PageSize)
    //         .ToListAsync();
    //     // return await _db.Games
    //     //     .Include(g => g.Genre)
    //     //     .Where(g => genre == null || g.Genre.Name == genre)
    //     //     .Skip(PageSize * (page - 1))
    //     //     .Take(PageSize)
    //     //     .ToListAsync();
    //     return null!;
    // }


    // public async void Get(int genreId, int pageNumber)
    // {
    //     if (genreId <= 0 || pageNumber < 0)
    //         return /*new GamesListViewModel()*/;
    //
    //     var games = await _db.Games.Include(g => g.Genre)
    //         .Where(g => g.Genre.Id == genreId)
    //         .Skip(PageSize * (pageNumber - 1))
    //         .Take(PageSize)
    //         .ToListAsync();
    //
    //     var genres = games.Select(g => g.Genre).Distinct().ToList();
    // }

    public async Task<Game?> GetGame(int id)
    {
        if (id <= 0)
            return null;

        return await _db.Games.Include(g => g.Genre).FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Game>> GetGames(int pageNumber)
    {
        if (pageNumber <= 0)
            return Enumerable.Empty<Game>();

        var games = await _db.Games
            .Include(g => g.Genre)
            .Skip(PageSize * (pageNumber - 1))
            .Take(PageSize).ToListAsync();

        return games;
    }

    public async Task<IEnumerable<Game>> GetGamesByGenre(int genreId, int pageNumber)
    {
        if (genreId <= 0 || pageNumber <= 0)
            return Enumerable.Empty<Game>();

        // var games = await _db.Games
        //     .Include(g => g.Genre)
        //     .Where(g => g.GenreId == genreId)
        //     .Skip(PageSize * (pageNumber - 1))
        //     .Take(PageSize)
        //     .ToListAsync();
        // return games;
        return (await _db.Genres
                   .Include(g => g.Games)
                   .FirstOrDefaultAsync(g => g.Id == genreId))?
               .Games.Skip(PageSize * (pageNumber - 1))
               .Take(PageSize)
               .ToList() ??
               Enumerable.Empty<Game>();
    }

    public Task<IEnumerable<Game>> GetGamesByGenre(string genre, int pageNumber)
    {
        if (string.IsNullOrWhiteSpace(genre))
            return Task.FromResult(Enumerable.Empty<Game>());

        var g = _db.Genres.FirstOrDefault(g => g.Name == genre);

        return g is not null 
            ? GetGamesByGenre(g.Id, pageNumber) 
            : Task.FromResult(Enumerable.Empty<Game>());
    }

    public async Task<IEnumerable<Genre>> GetGenres()
    {
        return await _db.Genres.ToListAsync();
    }
}