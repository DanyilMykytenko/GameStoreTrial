using Data.Entities;

namespace Data.Interfaces
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<IEnumerable<Basket>> GetAllWithDetails();
        Task<Basket> GetByIdWithDetails(int id);
    }
}
