using Business.Models;

namespace Business.Interfaces
{
    public interface IPublisherService : ICrudService<PublisherModel, CreateUpdatePublisherModel>
    {
        Task<PublisherModel> GetByGameAsync(string key);
        Task<PublisherModel> GetByCompanyNameAsync(string companyName);
    }
}
