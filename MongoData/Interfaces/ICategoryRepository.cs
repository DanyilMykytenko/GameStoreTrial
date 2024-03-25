using MongoData.Entities;

namespace MongoData.Interfaces
{
    public interface ICategoryRepository : ICrudMongo<Category>
    {
        Task<IEnumerable<Category>> GetByGameAsync(string key);
    }
}
