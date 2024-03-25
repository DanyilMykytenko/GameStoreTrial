using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(GameStoreDbContext ctx) : base(ctx)
        {
        }

        public async Task<ICollection<Order>> GetAllPaidOrders(int customerId)
        {
            var orders = await _ctx.Set<Order>().Where(x => x.isPaid == true && x.CustomerId == customerId).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetAllWithDetails()
        {
            var orders = await _ctx.Set<Order>().Include(x => x.Details).ToListAsync();
            return orders;
        }

        public async Task<Order> GetLastOrderWithDetails()
        {
            var order = await _ctx.Set<Order>().Include(x => x.Details).ToListAsync();
            return order.Last();
        }

        public async Task<Order> GetOrderByIdWithDetails(int id)
        {
            return await _ctx.Set<Order>().Include(x => x.Details).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
