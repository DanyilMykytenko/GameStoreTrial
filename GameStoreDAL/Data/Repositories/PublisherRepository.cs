using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(GameStoreDbContext ctx)
            : base(ctx) { }

        public async Task<Publisher> GetByCompanyNameAsync(string companyName)
        {
            var publisher = await _ctx.Set<Publisher>().FirstOrDefaultAsync(x => x.CompanyName == companyName);
            return publisher;
        }

        public async Task<Publisher> GetByGameAsync(string key)
        {
            var publisher = await _ctx.Set<Publisher>().FirstOrDefaultAsync(x => x.Games.Any(y => y.Key == key));
            return publisher;
        }

        public async Task<Publisher> GetByIdWithDetails(int id)
        {
            var publisher = await _ctx.Set<Publisher>()
            .Include(x => x.Games)
            .FirstOrDefaultAsync(x => x.Id == id);
            return publisher;
        }
    }
}
