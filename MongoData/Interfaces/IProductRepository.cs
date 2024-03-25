using MongoData.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoData.Interfaces
{
    public interface IProductRepository : ICrudMongo<Product>
    {
        Task<Product> GetByKeyAsync(string key);
        IMongoCollection<Product> GetCollection();
        IMongoQueryable<Product> GetQueryable();
        Task<ICollection<Product>> GetAllAsync(FilterDefinition<Product> expression);
        Task<IEnumerable<Product>> SortAsync(IMongoQueryable<Product> query);

        Task<IEnumerable<Product>> GetByGenreAsync(int id);
        Task<IEnumerable<Product>> GetByPublisherAsync(string companyName);
    }
}
