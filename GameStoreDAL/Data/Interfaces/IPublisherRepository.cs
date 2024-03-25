using Data.Entities;

namespace Data.Interfaces
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher> GetByGameAsync(string key);
        Task<Publisher> GetByCompanyNameAsync(string companyName);
        Task<Publisher> GetByIdWithDetails(int id);
    }
}
