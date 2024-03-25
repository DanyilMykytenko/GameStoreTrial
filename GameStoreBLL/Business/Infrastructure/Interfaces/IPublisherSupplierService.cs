using Business.Models;

namespace Business.Infrastructure.Interfaces
{
    public interface IPublisherSupplierService
    {
        Task DeleteAsync(int modelId);
        Task<IEnumerable<PublisherModel>> GetAllAsync();
        Task<PublisherModel> GetByIdAsync(int id);
        Task UpdateAsync(CreateUpdatePublisherModel model);
        Task<PublisherModel> GetByGameAsync(string key);
        Task<PublisherModel> GetByCompanyNameAsync(string companyName);
    }
}
