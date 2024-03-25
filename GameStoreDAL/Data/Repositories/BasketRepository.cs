using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        public BasketRepository(GameStoreDbContext ctx) : base(ctx)
        {
        }
        public async Task<Basket> GetByIdWithDetails(int id)
        {
            var basket = await _ctx.Set<Basket>().Include(x => x.Details).FirstOrDefaultAsync();
            return basket;
        }
        public async Task<IEnumerable<Basket>> GetAllWithDetails()
        {
            var basket = await _ctx.Set<Basket>().Include(x => x.Details).ToListAsync();
            return basket;
        }
    }
}
