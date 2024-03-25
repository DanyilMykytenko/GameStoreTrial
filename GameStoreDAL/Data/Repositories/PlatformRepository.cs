using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PlatformRepository : Repository<Platform>, IPlatformRepository
{
    public PlatformRepository(GameStoreDbContext ctx)
        : base(ctx)
    {
    }

    public async Task<IEnumerable<Platform>> GetByGameAsync(string key)
    {
        var platforms = await _ctx.Set<Platform>().Where(x => x.Games.Any(y => y.Key == key)).ToListAsync();
        return platforms;
    }
}
