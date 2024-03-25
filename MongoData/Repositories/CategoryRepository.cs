using MongoData.Entities;
using MongoData.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoData.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IMongoCollection<Product> _products;
        public CategoryRepository(IMongoDatabase database)
        {
            _categories = database.GetCollection<Category>(MongoCollectionNames.Categories);
            _products = database.GetCollection<Product>(MongoCollectionNames.Products);
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.Find(Builders<Category>.Filter.Empty).ToListAsync();
        }

        public async Task<ICollection<Category>> GetAllAsync(Expression<Func<Category, bool>> expression)
        {
            return await _categories.Find<Category>(expression).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetByGameAsync(string key)
        {
            var filter = Builders<Product>.Filter.Where(x => x.Key == key);
            var product = await _products.Find(filter).FirstOrDefaultAsync();
            
            var categoryFilter = Builders<Category>.Filter.Where(x => x.CategoryID == product.CategoryID);
            var categories = await _categories.Find(categoryFilter).ToListAsync();

            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var filter = Builders<Category>.Filter.Where(x => x.CategoryID == id);
            var category = await _categories.Find(filter).FirstOrDefaultAsync();
            return category;
        }

        public IMongoCollection<Category> GetCollection()
        {
            return _categories;
        }
    }
}
