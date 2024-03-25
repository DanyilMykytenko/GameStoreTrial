using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(GameStoreDbContext ctx)
        : base(ctx)
    {
    }

    public async Task<IEnumerable<Genre>> GetByGameAsync(string key)
    {
        var genres = await _ctx.Set<Genre>().Where(x => x.Games.Any(y => y.Key == key)).ToListAsync();
        return genres;
    }
}
