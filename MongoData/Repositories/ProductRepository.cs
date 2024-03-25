using MongoData.Entities;
using MongoData.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace MongoData.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Supplier> _suppliers;
        public ProductRepository(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>(MongoCollectionNames.Products);
            _suppliers = database.GetCollection<Supplier>(MongoCollectionNames.Suppliers);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(Builders<Product>.Filter.Empty).ToListAsync();
        }

        public async Task<ICollection<Product>> GetAllAsync(FilterDefinition<Product> expression)
        {
            return await _products.Find(expression).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var filter = Builders<Product>.Filter.Where(x => x.ProductID == id);
            var product = await _products.Find(filter).FirstOrDefaultAsync();
            //product.Viewed += 1;
            return product;
        }

        public async Task<Product> GetByKeyAsync(string key)
        {
            var filter = Builders<Product>.Filter.Where(x => x.Key == key);
            var product = await _products.Find(filter).FirstOrDefaultAsync();
            product.Viewed += 1;
            return product;
        }
        public IMongoCollection<Product> GetCollection()
        {
            return _products;
        }

        public async Task<IEnumerable<Product>> SortAsync(IMongoQueryable<Product> query)
        {
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByGenreAsync(int id)
        {
            var filter = Builders<Product>.Filter.Where(x => x.CategoryID == id);
            var products = await _products.Find(filter).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetByPublisherAsync(string companyName)
        {
            var supplierFilter = Builders<Supplier>.Filter.Where(x => x.CompanyName == companyName);
            var supplier = await _suppliers.Find(supplierFilter).FirstOrDefaultAsync();

            var filter = Builders<Product>.Filter.Where(x => x.SupplierID == supplier.SupplierID);
            var products = await _products.Find(filter).ToListAsync();

            return products;
        }

        public IMongoQueryable<Product> GetQueryable()
        {
            return _products.AsQueryable();
        }

        public async Task<ICollection<Product>> GetAllAsync(Expression<Func<Product, bool>> expression)
        {
            return await _products.Find(expression).ToListAsync();
        }
    }
}
