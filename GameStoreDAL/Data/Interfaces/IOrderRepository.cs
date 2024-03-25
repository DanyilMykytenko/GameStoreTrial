using Data.Entities;

namespace Data.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<ICollection<Order>> GetAllPaidOrders(int customerId);
        Task<Order> GetOrderByIdWithDetails(int id);
        Task<IEnumerable<Order>> GetAllWithDetails();
        Task<Order> GetLastOrderWithDetails();
    }
}
