using MongoData.Entities;

namespace MongoData.Interfaces
{
    public interface ISuppliersRepository : ICrudMongo<Supplier>
    {
        Task<Supplier> GetByGameAsync(string key);
        Task<Supplier> GetByCompanyNameAsync(string companyName);
    }
}
