using Business.Models;

namespace Business.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketModel>> GetAllAsync();

        Task<BasketModel> GetByIdAsync(int id);

        Task AddAsync(BasketModel model);

        Task UpdateAsync(BasketModel model);

        Task DeleteAsync(int modelId);
        Task<BasketModel> GetByIdWithDetails(int id);
        Task AddGameToBasket(string key);
        Task RemoveGameFromBasket(string key);
    }
}
